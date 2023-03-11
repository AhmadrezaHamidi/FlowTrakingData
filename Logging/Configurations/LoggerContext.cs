namespace Logging
{
    public class LoggerContext
    {
        public LoggerContext()
        {
            LogConfigurations = new Dictionary<string, object>();
        }

        public LogLevel MinimumLevel { get; set; } = LogLevel.Info;

        /// <summary>
        /// Delete logs before date
        /// </summary>
        public int? DeleteBefore { get; set; } = null;

        /// <summary>
        /// True to use UTC time rather than local time.
        /// Defaults to false.
        /// </summary>
        public bool UseUtcTime { get; set; }

        public Dictionary<string, object> LogConfigurations { get; set; }
    }
}