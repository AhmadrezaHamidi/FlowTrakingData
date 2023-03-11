namespace Logging
{
    public class DateTimeConfig
    {
        private readonly LogBuilder _builder;
        private readonly Action<bool> _timeFormat;

        public DateTimeConfig(LogBuilder builder, Action<bool> timeFormat)
        {
            _timeFormat = timeFormat;
            _builder = builder;
        }

        public LogBuilder Set(bool useUtcTime)
        {
            _timeFormat(useUtcTime);
            return _builder;
        }
    }
}