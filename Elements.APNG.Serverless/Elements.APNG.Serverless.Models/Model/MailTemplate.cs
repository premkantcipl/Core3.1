namespace Elements.APNG.Serverless.Models.Model
{
    public class MailTemplate
    {
        public string SendGridName { get; }

        public static MailTemplate Welcome(string version)
        { 
            return new MailTemplate($"Welcome{version}"); 
        }

        public static MailTemplate ResetPassword(string version) 
        { 
            return new MailTemplate($"ResetPassword{version}"); 
        } 

        public static MailTemplate InvoiceNotice(string version) 
        { 
            return new MailTemplate($"InvoiceNotice{version}"); 
        }

        public static MailTemplate CreditMemoNotice(string version)
        {
            return new MailTemplate($"CreditMemoNotice{version}");
        }

        public static MailTemplate PendingPayrollInvoice(string version) 
        { 
            return new MailTemplate($"PendingPayrollInvoice{version}"); 
        }

        public static MailTemplate EmployeeAdded(string version) 
        { 
            return new MailTemplate($"EmployeeAdded{version}"); 
        }

        public static MailTemplate CustomerAdded(string version) 
        {
            return new MailTemplate($"CustomerAdded{version}"); 
        }

        public static MailTemplate InvoiceAddedForReview(string version)
        { 
            return new MailTemplate($"InvoiceAddedForReview{version}"); 
        } 
    
        public static MailTemplate InvoiceDeclined(string version) 
        { 
            return new MailTemplate($"InvoiceDeclined{version}"); 
        }

        public static MailTemplate SubmitPayrollReminder(string version) 
        { 
            return new MailTemplate($"SubmitPayrollReminder{version}"); 
        }

        public static MailTemplate Reminder(string version) 
        { 
            return new MailTemplate($"Reminder{version}"); 
        }

        public static MailTemplate MissedPayment(string version) 
        {
            return new MailTemplate($"MissedPayment{version}"); 
        }

        public static MailTemplate UpdatedInvoiceStatus(string version)
        {
            return new MailTemplate($"UpdatedInvoiceStatus{version}");
        }

        public const string SUBJECT_PREFIX = "ApprovPay";

        public const string DATE_FORMAT = "dd MMM yyyy";

        private MailTemplate(string templateName)
        {
            SendGridName = SUBJECT_PREFIX + templateName;
        }
    }
}
