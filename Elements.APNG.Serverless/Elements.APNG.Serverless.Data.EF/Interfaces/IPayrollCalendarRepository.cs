using Elements.APNG.Serverless.Data.EF.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elements.APNG.Serverless.Data.EF.Interfaces
{
    public interface IPayrollCalendarRepository : IRepository<PayrollCalendar>
    {
        Task<IEnumerable<PayrollCalendar>> GetPayrollCalendars(DateTime start, DateTime end);
    }
}
