namespace Elements.APNG.Serverless.Models.ConfigurationModel
{
    public class CosmosConnection
    {

        /// <summary>
        ///     Endpoint Url
        /// </summary>
        public string EndpointUrl { get; set; }
        /// <summary>
        ///     Container primary Key
        /// </summary>
        public string PrimaryKey { get; set; }

        /// <summary>
        /// Database Name
        /// </summary>
        public string DatabaseName { get; set; }
    }
}
