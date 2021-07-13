using Elements.APNG.Serverless.Data.EF.Interfaces;
using Elements.APNG.Serverless.Data.EF.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elements.APNG.Serverless.Data.EF.Repository
{
    public class PayrollCalendarRepository : CosmosDbRepository<PayrollCalendar>, IPayrollCalendarRepository
    {
        /// <summary>
        ///     CosmosDB container name
        /// </summary>
        public override string ContainerName { get; } = "payrollCalendar";

        /// <summary>
        ///     Generate Id.
        ///     e.g. "shoppinglist:783dfe25-7ece-4f0b-885e-c0ea72135942"
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string GenerateId(PayrollCalendar entity) => $"{entity.Id}:{Guid.NewGuid()}";

        /// <summary>
        ///     Returns the value of the partition key
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey(entityId.Split(':')[0]);

        public async Task<IEnumerable<PayrollCalendar>> GetPayrollCalendars(DateTime startDate, DateTime endDate)
        {
            var query = "SELECT * FROM payrollCalendar pc";
            if (startDate != null && endDate != null)
            {
               
                    query = $"{query} WHERE";
                    bool firstFilter = true;
                  
                    if (startDate != null)
                    {
                        query = $"{query} {(firstFilter ? string.Empty : "AND")} pc.DueDateOfSubmissionOfChangeForm >= '{startDate:yyyy-MM-dd}'";
                        firstFilter = false;
                    }
                    if (endDate != null)
                    {
                        query = $"{query} {(firstFilter ? string.Empty : "AND")} pc.DueDateOfSubmissionOfChangeForm <= '{endDate:yyyy-MM-dd}'";
                    }
                    query = $"{query}";
              
            }

            return await this.GetItemsAsync(query);
        }

        public PayrollCalendarRepository(ICosmosDbContainerFactory factory) : base(factory)
        { }
    
    }
}
