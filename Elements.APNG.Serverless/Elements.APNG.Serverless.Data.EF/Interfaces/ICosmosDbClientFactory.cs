namespace Elements.APNG.Serverless.Data.EF.Interfaces
{
    public interface ICosmosDbClientFactory
    {
        ICosmosDbClient GetClient(string collectionName);
    }
}
