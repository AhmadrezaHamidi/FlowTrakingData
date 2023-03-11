namespace Logging
{
    public static class LogLevelExtensions
    {
        public static bool IsLevelEnabled(this string level, LogLevel contextLevel, LogLevel? logLevelStore)
        {
            if (logLevelStore is not null && level.LevelParser() < logLevelStore)
                return false;
            else if (logLevelStore is null && level.LevelParser() < contextLevel)
                return false;

            return true;
        }

        public static LogLevel LevelParser(this string level) => level.ToUpper() switch
        {
            "INFO" => LogLevel.Info,
            "WARNING" => LogLevel.Warning,
            "ERROR" => LogLevel.Error,
            "DEBUG" => LogLevel.Debug,
            "TRACE" => LogLevel.Trace,
            "FATAL" => LogLevel.Fatal,
            _ => throw new ArgumentOutOfRangeException()
        };

        public static string LevelDescriptor(this LogLevel level) => level switch
        {
            LogLevel.Info => "INFO",
            LogLevel.Warning => "WARNING",
            LogLevel.Error => "ERROR",
            LogLevel.Debug => "DEBUG",
            LogLevel.Trace => "TRACE",
            LogLevel.Fatal => "FATAL",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}