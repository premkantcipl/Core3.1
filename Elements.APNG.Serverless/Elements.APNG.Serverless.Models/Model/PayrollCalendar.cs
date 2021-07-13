using System;

namespace Elements.APNG.Serverless.Models.Model
{
    public class PayrollCalendar
    {
        public short PayrollDueType { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public DateTime DueDateOfSubmissionOfChangeForm { get; set; }
        public DateTime DueDateOfApprovalOfInvoice { get; set; }
        public DateTime DueDateOfPaymentOfInvoice { get; set; }
    }
}