namespace Logging
{
    public interface ILogReader<T> where T : class
    {
        Task<IList<Log<T>>> ReadAsync(DateTime date);

        Task<IList<Log<T>>> ReadAsync(DateTime date, LogLevel level);

        Task<IList<Log<T>>> ReadAsync(string category, DateTime date);

        Task<IList<Log<T>>> DebugAsync(DateTime date);

        Task<IList<Log<T>>> InfoAsync(DateTime date);

        Task<IList<Log<T>>> WarningAsync(DateTime date);

        Task<IList<Log<T>>> TraceAsync(DateTime date);

        Task<IList<Log<T>>> ErrorAsync(DateTime date);

        Task<IList<Log<T>>> FatalAsync(DateTime date);

        Task<IList<Log<T>>> ReadAsync(DateTime fromDate, DateTime toDate);

        Task<IList<Log<T>>> ReadAsync(DateTime fromDate, DateTime toDate, LogLevel level);

        Task<IList<Log<T>>> DebugAsync(DateTime fromDate, DateTime toDate);

        Task<IList<Log<T>>> InfoAsync(DateTime fromDate, DateTime toDate);

        Task<IList<Log<T>>> WarningAsync(DateTime fromDate, DateTime toDate);

        Task<IList<Log<T>>> TraceAsync(DateTime fromDate, DateTime toDate);

        Task<IList<Log<T>>> ErrorAsync(DateTime fromDate, DateTime toDate);

        Task<IList<Log<T>>> FatalAsync(DateTime fromDate, DateTime toDate);

        IList<Log<T>> Read(DateTime date);

        IList<Log<T>> Read(DateTime date, LogLevel level);

        IList<Log<T>> Read(string category, DateTime date);

        IList<Log<T>> Debug(DateTime date);

        IList<Log<T>> Info(DateTime date);

        IList<Log<T>> Warning(DateTime date);

        IList<Log<T>> Trace(DateTime date);

        IList<Log<T>> Error(DateTime date);

        IList<Log<T>> Fatal(DateTime date);

        IList<Log<T>> Read(DateTime fromDate, DateTime toDate);

        IList<Log<T>> Read(DateTime fromDate, DateTime toDate, LogLevel level);

        IList<Log<T>> Debug(DateTime fromDate, DateTime toDate);

        IList<Log<T>> Info(DateTime fromDate, DateTime toDate);

        IList<Log<T>> Warning(DateTime fromDate, DateTime toDate);

        IList<Log<T>> Trace(DateTime fromDate, DateTime toDate);

        IList<Log<T>> Error(DateTime fromDate, DateTime toDate);

        IList<Log<T>> Fatal(DateTime fromDate, DateTime toDate);
    }
}