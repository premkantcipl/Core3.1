using System;

namespace Elements.APNG.Serverless.Data.EF.Models
{
    public class Customer : Entity
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string Note { get; set; }
        public string LandPhone { get; set; }
        public int PayrollDue { get; set; }
        public int OtherDue { get; set; }
        public int PreferredPayment { get; set; }
        public int Status { get; set; }
    }
}
