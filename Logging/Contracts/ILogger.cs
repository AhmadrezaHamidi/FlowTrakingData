namespace Logging
{
    public interface ILogger<T> where T : class
    {
        void Info(T message, string traceId, string sectionId, string serviceId, string category = "");

        void Log(string level, T message, string traceId, string sectionId, string serviceId, string category = "");

        void Debug(T message, string traceId, string sectionId, string serviceId, string category = "");

        void Trace(T message, string traceId, string sectionId, string serviceId, string category = "");

        void Warning(T message, string traceId, string sectionId, string serviceId, string category = "");

        void Error(T message, string traceId, string sectionId, string serviceId, string category = "");

        void Fatal(T message, string traceId, string sectionId, string serviceId, string category = "");

        Task InfoAsync(T message, string traceId, string sectionId, string serviceId, string category = "");

        Task LogAsync(string level, T message, string traceId, string sectionId, string serviceId, string category = "");

        Task DebugAsync(T message, string traceId, string sectionId, string serviceId, string category = "");

        Task TraceAsync(T message, string traceId, string sectionId, string serviceId, string category = "");

        Task WarningAsync(T message, string traceId, string sectionId, string serviceId, string category = "");

        Task ErrorAsync(T message, string traceId, string sectionId, string serviceId, string category = "");

        Task FatalAsync(T message, string traceId, string sectionId, string serviceId, string category = "");
    }
}