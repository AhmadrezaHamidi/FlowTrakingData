namespace Logging
{
    public class Log<T> : ILog<T> where T : class
    {
        public Log()
        {
            var typeFullName = typeof(T).ToString();
            var temp = typeFullName.Split('.');
            Category = temp[temp.Length - 1];
        }

        public Log(string category) => Category = category;

        public Log(LogLevel level) => Level = nameof(level);

        public Log(LogLevel level, string category)
        {
            Category = category;
            Level = nameof(level);
        }

        public DateTime LogDate { get; set; }

        public string Level { get; set; }

        public T Message { get; set; }

        public string Category { get; set; }

        public string TraceId { get; set; }

        public string SectionId { get; set; }

        public string ServiceId { get; set; }

        public override string ToString()
        {
            var padding = new string(' ', (9 - Level.Length));
            var result = $"{LogDate:yyyy-MM-dd HH:mm:ss}, [{Category}] - {Level}{padding} {Message};";
            return result;
        }
    }

    public class Log : Log<string>
    {
        public Log()
        {
        }

        public Log(string category) : base(category)
        {
        }

        public Log(LogLevel level, string category) : base(level, category)
        {
        }

        public Log(LogLevel level) : base(level)
        {
        }

        public override string ToString()
        {
            var padding = new string(' ', (6 - Level.Length));
            string result;

            if (!string.IsNullOrEmpty(Category))
            {
                result = $"{LogDate:yyyy-MM-dd HH:mm:ss}, [{Category}] - {Level}{padding} {Message};";
            }
            else
            {
                result = $"{LogDate:yyyy-MM-dd HH:mm:ss} - {Level}{padding} {Message};";
            }
            return result;
        }
    }
}