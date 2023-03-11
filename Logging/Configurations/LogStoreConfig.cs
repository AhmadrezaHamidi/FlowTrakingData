namespace Logging
{
    public class LogStoreConfig
    {
        private readonly LogBuilder _builder;
        private readonly Action<ILogStore, string> _actionLogStore;

        public LoggerContext LoggerContext { get; private set; }

        public LogStoreConfig(LogBuilder builder, LoggerContext context, Action<ILogStore, string> actionLogStore)
        {
            _builder = builder;
            _actionLogStore = actionLogStore;
            LoggerContext = context;
        }

        public LogBuilder Store(ILogStore logStore)
        {
            _actionLogStore(logStore, logStore.Name);
            return _builder;
        }
    }
}