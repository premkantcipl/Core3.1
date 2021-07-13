namespace Elements.APNG.Serverless.Models.Model
{
    public class EmailConfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SendGridApiKey { get; set; }
        public string InvoiceApproveDeclineLink { get; set; }
        public string MailTemplateVersion { get; set; }
        public string ReturnUrl { get; set; }

    }
}
