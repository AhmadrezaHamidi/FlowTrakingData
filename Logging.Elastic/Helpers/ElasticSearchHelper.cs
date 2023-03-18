using Elasticsearch.Net;
using Nest;

namespace Logging.Elastic
{
    public static class ElasticSearchHelper
    {
        public static ElasticClient GetESClient(string connection, string username, string password)
        {
            ConnectionSettings connectionSettings;
            ElasticClient elasticClient;
            StaticConnectionPool connectionPool;
            //Provide your ES cluster addresses
            var nodes = new Uri[] { new Uri(connection) };
            connectionPool = new StaticConnectionPool(nodes);
            connectionSettings = new ConnectionSettings(connectionPool)
                .PrettyJson()
                .DefaultIndex(nameof(Log))
                .BasicAuthentication(username, password);
            elasticClient = new ElasticClient(connectionSettings);
            CreateIndex(elasticClient, nameof(Log).ToLower());
            return elasticClient;
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            if (!client.Indices.Exists(indexName).Exists)
            {
                var result = client.Indices.Create(indexName.ToLower(), i => i.Map<Log>(x => x.AutoMap()));
                if (!result.IsValid)
                    throw new Exception(result.OriginalException.ToString());
            }
        }
    }
}