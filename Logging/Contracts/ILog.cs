namespace Logging
{
    public interface ILog<T> where T : class
    {
        DateTime LogDate { get; set; }

        string Level { get; set; }

        T Message { get; set; }

        /// <summary>
        /// business category section
        /// </summary>
        string Category { get; set; }

        /// <summary>
        /// version trace id for debug and trace
        /// </summary>
        string TraceId { get; set; }

        /// <summary>
        /// flow id auto generate for every flow
        /// </summary>
        string SectionId { get; set; }

        /// <summary>
        /// microservice id auto generated for unique flow
        /// </summary>
        string ServiceId { get; set; }
    }
}