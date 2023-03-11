namespace Logging.Console
{
    public static class LogStoreConfigExtension
    {
        private static readonly string _storeName = $"{nameof(ConsoleLogStore)}-{Guid.NewGuid():N}";

        public static LogBuilder Console(this LogStoreConfig config, LogLevel? level)
        {
            var consoleConfig = new ConsoleLogConfig
            {
                Level = level
            };
            config.LoggerContext.LogConfigurations.Add(_storeName, consoleConfig);
            return config.Store(new ConsoleLogStore(_storeName, config.LoggerContext));
        }

        public static LogBuilder Console(this LogStoreConfig config)
        {
            var consoleConfig = new ConsoleLogConfig();
            config.LoggerContext.LogConfigurations.Add(_storeName, consoleConfig);
            return config.Store(new ConsoleLogStore(_storeName, config.LoggerContext));
        }
    }
}