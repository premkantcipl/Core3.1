using Microsoft.Azure.Cosmos;

namespace Elements.APNG.Serverless.Data.EF.Interfaces
{
    public interface ICosmosDbContainer
    {
        /// <summary>
        ///     Instance of Azure Cosmos DB Container class
        /// </summary>
        Container _container { get; }
    }
}
