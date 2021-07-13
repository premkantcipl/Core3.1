using Elements.APNG.Serverless.Data.EF.Interfaces;
using Microsoft.Azure.Cosmos;

namespace Elements.APNG.Serverless.Data.EF
{
    public class CosmosDbContainer : ICosmosDbContainer
    {
        public Container _container { get; }

        public CosmosDbContainer(CosmosClient cosmosClient,
                                 string databaseName,
                                 string containerName)
        {
            this._container = cosmosClient.GetContainer(databaseName, containerName);
        }
    }
}
