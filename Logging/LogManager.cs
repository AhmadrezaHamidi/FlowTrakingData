using Core;

namespace Logging
{
    public class LogManager<T> : ILogReader<T>, ILogger<T> where T : class
    {
        private readonly Dictionary<string, ILogStore> _logStores = new();
        private readonly LoggerContext _logContext;

        public LogManager(Dictionary<string, ILogStore> logStores, LoggerContext logContext)
        {
            _logStores = logStores;
            _logContext = logContext;
        }

        #region Read

        #region Async

        public async Task<IList<Log<T>>> DebugAsync(DateTime date)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(await log.ReadAsync<T>(date, LogLevel.Debug.LevelDescriptor()));
            }
            return logs;
        }

        public async Task<IList<Log<T>>> DebugAsync(DateTime fromDate, DateTime toDate)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(await log.ReadAsync<T>(fromDate, toDate, LogLevel.Debug.LevelDescriptor()));
            }
            return logs;
        }

        public async Task<IList<Log<T>>> ReadAsync(DateTime date)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(await log.ReadAsync<T>(date));
            }
            return logs;
        }

        public async Task<IList<Log<T>>> ReadAsync(DateTime date, LogLevel level)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(await log.ReadAsync<T>(date, level.LevelDescriptor()));
            }
            return logs;
        }

        public async Task<IList<Log<T>>> ReadAsync(string category, DateTime date)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(await log.ReadAsync<T>(category, date));
            }
            return logs;
        }

        public async Task<IList<Log<T>>> ReadAsync(DateTime fromDate, DateTime toDate)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(await log.ReadAsync<T>(fromDate, toDate));
            }
            return logs;
        }

        public async Task<IList<Log<T>>> ReadAsync(DateTime fromDate, DateTime toDate, LogLevel level)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(await log.ReadAsync<T>(fromDate, toDate, level.LevelDescriptor()));
            }
            return logs;
        }

        public async Task<IList<Log<T>>> InfoAsync(DateTime date)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(await log.ReadAsync<T>(date, LogLevel.Info.LevelDescriptor()));
            }
            return logs;
        }

        public async Task<IList<Log<T>>> WarningAsync(DateTime date)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(await log.ReadAsync<T>(date, LogLevel.Warning.LevelDescriptor()));
            }
            return logs;
        }

        public async Task<IList<Log<T>>> TraceAsync(DateTime date)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(await log.ReadAsync<T>(date, LogLevel.Trace.LevelDescriptor()));
            }
            return logs;
        }

        public async Task<IList<Log<T>>> ErrorAsync(DateTime date)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(await log.ReadAsync<T>(date, LogLevel.Error.LevelDescriptor()));
            }
            return logs;
        }

        public async Task<IList<Log<T>>> FatalAsync(DateTime date)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(await log.ReadAsync<T>(date, LogLevel.Fatal.LevelDescriptor()));
            }
            return logs;
        }

        public async Task<IList<Log<T>>> InfoAsync(DateTime fromDate, DateTime toDate)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(await log.ReadAsync<T>(fromDate, toDate, LogLevel.Info.LevelDescriptor()));
            }
            return logs;
        }

        public async Task<IList<Log<T>>> WarningAsync(DateTime fromDate, DateTime toDate)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(await log.ReadAsync<T>(fromDate, toDate, LogLevel.Error.LevelDescriptor()));
            }
            return logs;
        }

        public async Task<IList<Log<T>>> TraceAsync(DateTime fromDate, DateTime toDate)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(await log.ReadAsync<T>(fromDate, toDate, LogLevel.Trace.LevelDescriptor()));
            }
            return logs;
        }

        public async Task<IList<Log<T>>> ErrorAsync(DateTime fromDate, DateTime toDate)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(await log.ReadAsync<T>(fromDate, toDate, LogLevel.Error.LevelDescriptor()));
            }
            return logs;
        }

        public async Task<IList<Log<T>>> FatalAsync(DateTime fromDate, DateTime toDate)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(await log.ReadAsync<T>(fromDate, toDate, LogLevel.Fatal.LevelDescriptor()));
            }
            return logs;
        }

        #endregion Async

        #region Sync

        public IList<Log<T>> Debug(DateTime date)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(log.Read<T>(date, LogLevel.Debug.LevelDescriptor()));
            }
            return logs;
        }

        public IList<Log<T>> Debug(DateTime fromDate, DateTime toDate)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(log.Read<T>(fromDate, toDate, LogLevel.Debug.LevelDescriptor()));
            }
            return logs;
        }

        public IList<Log<T>> Read(DateTime date)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(log.Read<T>(date));
            }
            return logs;
        }

        public IList<Log<T>> Read(DateTime date, LogLevel level)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(log.Read<T>(date, level.LevelDescriptor()));
            }
            return logs;
        }

        public IList<Log<T>> Read(string category, DateTime date)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(log.Read<T>(category, date));
            }
            return logs;
        }

        public IList<Log<T>> Read(DateTime fromDate, DateTime toDate)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(log.Read<T>(fromDate, toDate));
            }
            return logs;
        }

        public IList<Log<T>> Read(DateTime fromDate, DateTime toDate, LogLevel level)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(log.Read<T>(fromDate, toDate, level.LevelDescriptor()));
            }
            return logs;
        }

        public IList<Log<T>> Info(DateTime date)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(log.Read<T>(date, LogLevel.Info.LevelDescriptor()));
            }
            return logs;
        }

        public IList<Log<T>> Warning(DateTime date)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(log.Read<T>(date, LogLevel.Warning.LevelDescriptor()));
            }
            return logs;
        }

        public IList<Log<T>> Trace(DateTime date)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(log.Read<T>(date, LogLevel.Trace.LevelDescriptor()));
            }
            return logs;
        }

        public IList<Log<T>> Error(DateTime date)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(log.Read<T>(date, LogLevel.Error.LevelDescriptor()));
            }
            return logs;
        }

        public IList<Log<T>> Fatal(DateTime date)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(log.Read<T>(date, LogLevel.Fatal.LevelDescriptor()));
            }
            return logs;
        }

        public IList<Log<T>> Info(DateTime fromDate, DateTime toDate)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(log.Read<T>(fromDate, toDate, LogLevel.Info.LevelDescriptor()));
            }
            return logs;
        }

        public IList<Log<T>> Warning(DateTime fromDate, DateTime toDate)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(log.Read<T>(fromDate, toDate, LogLevel.Warning.LevelDescriptor()));
            }
            return logs;
        }

        public IList<Log<T>> Trace(DateTime fromDate, DateTime toDate)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(log.Read<T>(fromDate, toDate, LogLevel.Trace.LevelDescriptor()));
            }
            return logs;
        }

        public IList<Log<T>> Error(DateTime fromDate, DateTime toDate)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(log.Read<T>(fromDate, toDate, LogLevel.Error.LevelDescriptor()));
            }
            return logs;
        }

        public IList<Log<T>> Fatal(DateTime fromDate, DateTime toDate)
        {
            var logs = new List<Log<T>>();
            foreach (var log in _logStores.Values)
            {
                logs.AddRange(log.Read<T>(fromDate, toDate, LogLevel.Fatal.LevelDescriptor()));
            }
            return logs;
        }

        #endregion Sync

        #endregion Read

        #region Write

        #region Async

        public async Task LogAsync(string level, T message, string traceId, string sectionId, string serviceId, string category = "")
        {
            Log<T> log;

            var logDate = DateTime.Now;

            if (_logContext.UseUtcTime)
            {
                logDate = DateTime.UtcNow;
            }
            if (category.IsNullOrEmpty())
            {
                log = new Log<T>
                {
                    Level = level.ToUpper(),
                    Message = message,
                    LogDate = logDate,
                    TraceId = traceId,
                    SectionId = sectionId,
                    ServiceId = serviceId,
                };
            }
            else
            {
                log = new Log<T>(category)
                {
                    Level = level.ToUpper(),
                    Message = message,
                    LogDate = logDate,
                    TraceId = traceId,
                    SectionId = sectionId,
                    ServiceId = serviceId,
                };
            }

            foreach (var logStore in _logStores)
            {
                await logStore.Value.WriteAsync(log);
            }
        }

        public async Task InfoAsync(T message, string traceId, string sectionId, string serviceId, string category = "") => await LogAsync(nameof(LogLevel.Info), message, traceId, sectionId, serviceId, category);

        public async Task DebugAsync(T message, string traceId, string sectionId, string serviceId, string category = "") => await LogAsync(nameof(LogLevel.Debug), message, traceId, sectionId, serviceId, category);

        public async Task ErrorAsync(T message, string traceId, string sectionId, string serviceId, string category = "") => await LogAsync(nameof(LogLevel.Error), message, traceId, sectionId, serviceId, category);

        public async Task FatalAsync(T message, string traceId, string sectionId, string serviceId, string category = "") => await LogAsync(nameof(LogLevel.Fatal), message, traceId, sectionId, serviceId, category);

        public async Task TraceAsync(T message, string traceId, string sectionId, string serviceId, string category = "") => await LogAsync(nameof(LogLevel.Trace), message, traceId, sectionId, serviceId, category);

        public async Task WarningAsync(T message, string traceId, string sectionId, string serviceId, string category = "") => await LogAsync(nameof(LogLevel.Warning), message, traceId, sectionId, serviceId, category);

        #endregion Async

        #region Sync

        public void Log(string level, T message, string traceId, string sectionId, string serviceId, string category = "")
        {
            Log<T> log;

            var logDate = DateTime.Now;

            if (_logContext.UseUtcTime)
            {
                logDate = DateTime.UtcNow;
            }
            if (category.IsNullOrEmpty())
            {
                log = new Log<T>
                {
                    Level = level,
                    Message = message,
                    LogDate = logDate,
                    TraceId = traceId,
                    SectionId = sectionId,
                    ServiceId = serviceId,
                };
            }
            else
            {
                log = new Log<T>(category)
                {
                    Level = level,
                    Message = message,
                    LogDate = logDate,
                    TraceId = traceId,
                    SectionId = sectionId,
                    ServiceId = serviceId,
                };
            }

            foreach (var logStore in _logStores)
            {
                logStore.Value.Write(log);
            }
        }

        public void Info(T message, string traceId, string sectionId, string serviceId, string category = "") => Log(nameof(LogLevel.Info), message, traceId, sectionId, serviceId, category);

        public void Debug(T message, string traceId, string sectionId, string serviceId, string category = "") => Log(nameof(LogLevel.Debug), message, traceId, sectionId, serviceId, category);

        public void Error(T message, string traceId, string sectionId, string serviceId, string category = "") => Log(nameof(LogLevel.Error), message, traceId, sectionId, serviceId, category);

        public void Fatal(T message, string traceId, string sectionId, string serviceId, string category = "") => Log(nameof(LogLevel.Fatal), message, traceId, sectionId, serviceId, category);

        public void Trace(T message, string traceId, string sectionId, string serviceId, string category = "") => Log(nameof(LogLevel.Trace), message, traceId, sectionId, serviceId, category);

        public void Warning(T message, string traceId, string sectionId, string serviceId, string category = "") => Log(nameof(LogLevel.Warning), message, traceId, sectionId, serviceId, category);

        #endregion Sync

        #endregion Write
    }
}