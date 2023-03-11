namespace Logging
{
    public interface ILogStore
    {
        public string Name { get; }

        Task<IList<Log<T>>> ReadAsync<T>(DateTime date) where T : class;

        Task<IList<Log<T>>> ReadAsync<T>(DateTime date, string level) where T : class;

        Task<IList<Log<T>>> ReadAsync<T>(string category, DateTime date) where T : class;

        Task<IList<Log<T>>> ReadAsync<T>(DateTime fromDate, DateTime toDate) where T : class;

        Task<IList<Log<T>>> ReadAsync<T>(DateTime fromDate, DateTime toDate, string level) where T : class;

        Task WriteAsync<T>(ILog<T> log) where T : class;

        IList<Log<T>> Read<T>(DateTime date) where T : class;

        IList<Log<T>> Read<T>(DateTime date, string level) where T : class;

        IList<Log<T>> Read<T>(string category, DateTime date) where T : class;

        IList<Log<T>> Read<T>(DateTime fromDate, DateTime toDate) where T : class;

        IList<Log<T>> Read<T>(DateTime fromDate, DateTime toDate, string level) where T : class;

        void Write<T>(ILog<T> log) where T : class;
    }
}