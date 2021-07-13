using Elements.APNG.Serverless.Models.Model;
using Elements.APNG.Serverless.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Elements.APNG.Serverless.Services.MailTemplateServices
{
    public class InvoiceMailTemplateService : IInvoiceMailTemplateService
    {

        private readonly ILogger<InvoiceMailTemplateService> _loggerService;
        private readonly EmailConfiguration _emailConfiguration;
        private readonly IEmailSender _emailSender;

        public InvoiceMailTemplateService(

            ILogger<InvoiceMailTemplateService> loggerService,
            IEmailSender emailSender
            )
        {
            _loggerService = loggerService;
            _emailSender = emailSender;
            _emailConfiguration = EnvironmentVariables.GetEmailConfigration();
        }

        public Task SendSubmitPayrollReminderAsync(Dictionary<string, DateTime> customersToRemind)
        {
            List<Task> listOfTasks = new List<Task>();

            try
            {
                string subjectPrefix = EnvironmentVariables.ServiceEnvironment() == "production" ? MailTemplate.SUBJECT_PREFIX : $"{MailTemplate.SUBJECT_PREFIX} ({CultureInfo.CurrentCulture.TextInfo.ToTitleCase(EnvironmentVariables.ServiceEnvironment())})";
                foreach (KeyValuePair<string, DateTime> keyValuePair in customersToRemind)
                {
                    MailTemplateMessage mailTemplateMessage = new MailTemplateMessage(keyValuePair.Key, $"{subjectPrefix} - Submit Payroll Reminder", MailTemplate.SubmitPayrollReminder(_emailConfiguration.MailTemplateVersion), new Dictionary<string, object> { { "PayrollCalendarDueDate", keyValuePair.Value.ToString(MailTemplate.DATE_FORMAT) } });

                    // do not call await within foreach loop and use list of tasks instead
                    // ref https://medium.com/@t.masonbarneydev/iterating-asynchronously-how-to-use-async-await-with-foreach-in-c-d7e6d21f89fa
                    listOfTasks.Add(_emailSender.SendEmailAsync(mailTemplateMessage));
                }

                return Task.WhenAll(listOfTasks);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, ex.Message.ToString());
            }

            // needs to return a task (in a case when an exception occurs)
            return Task.FromResult(true);
        }

        private static IEnumerable<string> GetCustomerMailAddresses(string customerEmailAddresses)
        {
            return customerEmailAddresses.Split(",");
        }

    }
}
