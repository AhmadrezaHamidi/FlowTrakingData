namespace Logging
{
    public class MinimumLevelConfig
    {
        private readonly LogBuilder _builder;
        private readonly Action<LogLevel> _action;

        public MinimumLevelConfig(LogBuilder builder, Action<LogLevel> action)
        {
            _builder = builder;
            _action = action;
        }

        public LogBuilder Debug()
        {
            _action(LogLevel.Debug);
            return _builder;
        }

        public LogBuilder Info()
        {
            _action(LogLevel.Info);
            return _builder;
        }

        public LogBuilder Warning()
        {
            _action(LogLevel.Warning);
            return _builder;
        }

        public LogBuilder Error()
        {
            _action(LogLevel.Error);
            return _builder;
        }

        public LogBuilder Trace()
        {
            _action(LogLevel.Trace);
            return _builder;
        }

        public LogBuilder Fatal()
        {
            _action(LogLevel.Fatal);
            return _builder;
        }
    }
}