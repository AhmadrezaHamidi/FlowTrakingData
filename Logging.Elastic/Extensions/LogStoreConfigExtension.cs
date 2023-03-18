namespace Logging.Elastic
{
    public static class LogStoreConfigExtension

    {
        private static readonly string _storeName = $"{nameof(ElasticSearchLogStore)}-{Guid.NewGuid():N}";

        public static LogBuilder ElasticSearch(this LogStoreConfig config, string connection, string username, string password, LogLevel level)
        {
            var elasticConfig = new ElasticSearchLogConfig()
            {
                Level = level,
                ConnectionString = connection,
            };

            config.LoggerContext.LogConfigurations.Add(_storeName, elasticConfig);
            return config.Store(new ElasticSearchLogStore(_storeName, ElasticSearchHelper.GetESClient(elasticConfig.ConnectionString, username, password), config.LoggerContext));
        }

        public static LogBuilder ElasticSearch(this LogStoreConfig config, string connection, string username, string password)

        {
            var elasticConfig = new ElasticSearchLogConfig()
            {
                ConnectionString = connection,
            };
            config.LoggerContext.LogConfigurations.Add(_storeName, elasticConfig);
            return config.Store(new ElasticSearchLogStore(_storeName, ElasticSearchHelper.GetESClient(elasticConfig.ConnectionString, username, password), config.LoggerContext));
        }
    }
}