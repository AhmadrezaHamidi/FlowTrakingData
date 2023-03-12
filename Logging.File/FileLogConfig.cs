namespace Logging.File
{
    public class FileLogConfig
    {
        private string _templateName;

        /// <summary>
        /// Name where to create the logs.
        /// Defaults to a local "logs" directory.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// File structure template name
        /// </summary>
        public string TemplateName
        {
            get
            {
                return _templateName;
            }
            private set
            {
                _templateName = RollingInterval switch
                {
                    RollingInterval.Day => "@TimeStamp",
                    RollingInterval.Month => "@Timestamp-@Level-@Category",
                    RollingInterval.Year => "@Timestamp-@Level-@Category",
                    _ => "@Timestamp"
                };
            }
        }

        /// <summary>
        /// Interval for seperate files
        /// </summary>
        public RollingInterval RollingInterval { get; set; } = RollingInterval.Day;

        /// <summary>
        /// Labels enabled to be logged by the library.
        /// An attempt to log with a label that is not enabled is simply ignored, no error is raised.
        /// Leave it empty or null to enable any label, which is the default.
        /// </summary>
        public LogLevel? Level { get; set; } = null;
    }
}