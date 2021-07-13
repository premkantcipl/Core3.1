using Elements.APNG.Serverless.Data.EF.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elements.APNG.Serverless.Data.EF.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetCustomers();
    }
}
