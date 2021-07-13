using System;

namespace Elements.APNG.Serverless.Models.Model
{
    public class Customer
    {
        public Guid Id { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public Guid? BillingAddressId { get; set; }
        public Guid? ShippingAddressId { get; set; }
        public string Note { get; set; }
        public string TaxIdentifier { get; set; }
        public string LandPhone { get; set; }
        public string MobilePhone { get; set; }
        public string FaxPhone { get; set; }
        public string OtherPhone { get; set; }
        public int BillingCurrency { get; set; }
        public int Location { get; set; }
        public int Class { get; set; }
        public int PayrollDue { get; set; }
        public int OtherDue { get; set; }
        public int PreferredPayment { get; set; }
        public int Status { get; set; }
        public DateTime? RenewalEffectiveDate { get; set; }
        public int HrisIncludedInAdmin { get; set; }
        public string QBClientId { get; set; }
        public string PONumber { get; set; }
        public Guid ModifiedBy { get; set; }
        public bool? PayrollApprovalRequired { get; set; }
    }
}
