using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elements.APNG.Serverless.Services.Interfaces
{
    public interface IInvoiceMailTemplateService
    {
        /// <summary>
        /// to send payroll submit reminder
        /// </summary>
        /// <param name="customersToRemind"></param>
        /// <returns></returns>
        Task SendSubmitPayrollReminderAsync(Dictionary<string, DateTime> customersToRemind);
    }
}
