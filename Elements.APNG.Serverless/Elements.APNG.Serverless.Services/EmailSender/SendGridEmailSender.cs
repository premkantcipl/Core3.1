using Elements.APNG.Serverless.Models.Model;
using Elements.APNG.Serverless.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Elements.APNG.Serverless.Services.EmailSender
{
    public class SendGridEmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfiguration;
        private readonly SendGridClient _sendGridClient;
        private readonly ILogger<SendGridEmailSender> _loggerService;

        public SendGridEmailSender(IOptions<EmailConfiguration> emailConfiguration, ILogger<SendGridEmailSender> loggerService)
        {
            _emailConfiguration = EnvironmentVariables.GetEmailConfigration();
            _sendGridClient = new SendGridClient(_emailConfiguration.SendGridApiKey);
            _loggerService = loggerService;
        }

        public async Task SendEmailAsync(MailTemplateMessage mailTemplateMessage) 
        {
            var guid = Guid.NewGuid();
            _loggerService.LogInformation($"Email is being sent. Guid: {guid} - MailTemplateMessage: {JsonConvert.SerializeObject(mailTemplateMessage)}");
            mailTemplateMessage.AddParameter("subject", mailTemplateMessage.Subject);

            string templateId = await GetTemplateId(mailTemplateMessage.Template.SendGridName);

            var sendGridMessage = MailHelper.CreateSingleTemplateEmailToMultipleRecipients(new EmailAddress(_emailConfiguration.From),
                                                                                           mailTemplateMessage.To.Select(x => new EmailAddress(x.Address)).ToList(),
                                                                                           templateId,
                                                                                           mailTemplateMessage.TemplateParameters);

            if (mailTemplateMessage.Attachments != null)
                AttachRangeTo(sendGridMessage, mailTemplateMessage.Attachments);

            Response response = await _sendGridClient.SendEmailAsync(sendGridMessage);

            _loggerService.LogInformation($"Email is sent. Guid: {guid} - Response: {response.StatusCode}");

            await ThrowExceptionIfNotOk(response);

        }

        private void AttachRangeTo(SendGridMessage sendGridMessage, IEnumerable<Models.Model.Attachment> attachments)
        {
            foreach (var attachment in attachments) 
            {
                sendGridMessage.AddAttachment(attachment.FileName, Convert.ToBase64String(attachment.FileContent, 0, attachment.FileContent.Length));
            }
        }

        private async Task<string> GetTemplateId(string templateName) 
        {
            var response = await _sendGridClient.RequestAsync(method: BaseClient.Method.GET, urlPath: "templates?generations=dynamic");
            await ThrowExceptionIfNotOk(response);
            string json = await response.Body.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(json);
            string templateId = ((IEnumerable<dynamic>)result.templates).Where(t => t.name == templateName).FirstOrDefault().id;

            if (string.IsNullOrEmpty(templateId))
                throw new Exception($"dynamic template with given name: {templateName} was not found in sendgrid");

            return templateId;
        }

        private async Task ThrowExceptionIfNotOk(Response response) 
        {
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Accepted)
            {
                var body = await response.Body.ReadAsStringAsync();
                throw new Exception($"SendGridEmailSender: Error - Message: {body} HttpStatusCode: {response.StatusCode} ErrorBody:{await response.Body.ReadAsStringAsync()}");
            }
        }
    }
}
