using Elements.APNG.Serverless.Data.EF.Models;
using Microsoft.Azure.Cosmos;

namespace Elements.APNG.Serverless.Data.EF.Interfaces
{
    /// <summary>
    ///  Defines the container level context
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IContainerContext<T> where T : Entity
    {
        string ContainerName { get; }
        string GenerateId(T entity);
        PartitionKey ResolvePartitionKey(string entityId);
    }
}
