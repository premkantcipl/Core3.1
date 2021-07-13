using Elements.APNG.Serverless.Data.EF.Models;
using Microsoft.Azure.Documents;

namespace Elements.APNG.Serverless.Data.EF.Interfaces
{
    public interface IDocumentCollectionContext<in T> where T : Entity
    {
        string CollectionName { get; }

        string GenerateId(T entity);

        PartitionKey ResolvePartitionKey(string entityId);
    }
}
