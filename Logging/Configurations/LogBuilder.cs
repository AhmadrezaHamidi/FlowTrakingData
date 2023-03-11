namespace Logging
{
    public class LogBuilder
    {
        private readonly Dictionary<string, ILogStore> _logStores = new();
        private readonly LoggerContext _loggerContext = new();
        private bool _isLoggerCreated;

        public LogBuilder()
        {
            MinimumLevel = new(this, delegate (LogLevel level)
            {
                _loggerContext.MinimumLevel = level;
            });

            WriteTo = new(this, _loggerContext, delegate (ILogStore logStore, string key)
            {
                _logStores.Add(key, logStore);
            });

            DateTime = new(this, delegate (bool useUtcTime)
            {
                _loggerContext.UseUtcTime = useUtcTime;
            });

            DeleteBefore = new(this, delegate (int day)
            {
                _loggerContext.DeleteBefore = day;
            });
        }

        public MinimumLevelConfig MinimumLevel { get; private set; }

        public LogStoreConfig WriteTo { get; private set; }

        public DateTimeConfig DateTime { get; private set; }

        public DeleteDateConfig DeleteBefore { get; private set; }

        public LogManager<T> CreateLogger<T>() where T : class
        {
            if (_isLoggerCreated)
                throw new InvalidOperationException("CreateLogger() was previously called and can only be called once.");
            _isLoggerCreated = true;

            return new LogManager<T>(_logStores, _loggerContext);
        }

        public LogManager<string> CreateLogger()
        {
            return CreateLogger<string>();
        }
    }
}