using Elements.APNG.Serverless.Data.EF.Interfaces;
using Elements.APNG.Serverless.Data.EF.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elements.APNG.Serverless.Data.EF.Repository
{
    public class CustomerRepository : CosmosDbRepository<Customer>, ICustomerRepository
    {
        /// <summary>
        ///     CosmosDB container name
        /// </summary>
        public override string ContainerName { get; } = "payrollReminder";

        /// <summary>
        ///     Generate Id.
        ///     e.g. "shoppinglist:783dfe25-7ece-4f0b-885e-c0ea72135942"
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string GenerateId(Customer entity) => $"{entity.Id}:{Guid.NewGuid()}";

        /// <summary>
        ///     Returns the value of the partition key
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey(entityId.Split(':')[0]);

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var query = "SELECT * FROM payrollReminder c";

            return await this.GetItemsAsync(query);
        }

        public CustomerRepository(ICosmosDbContainerFactory factory) : base(factory)
        { }

    }
}
