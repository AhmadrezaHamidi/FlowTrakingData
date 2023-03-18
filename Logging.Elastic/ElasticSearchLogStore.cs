using Nest;

namespace Logging.Elastic
{
    public class ElasticSearchLogStore : ILogStore
    {
        private readonly IElasticClient _elasticClient;
        private readonly LoggerContext _context;
        private readonly ElasticSearchLogConfig _elasticSearchLogConfig;

        public ElasticSearchLogStore(string name, IElasticClient elasticClient, LoggerContext context)
        {
            Name = name;
            _elasticClient = elasticClient;
            _elasticSearchLogConfig = (ElasticSearchLogConfig)context.LogConfigurations[Name];
            _context = context;
        }

        public string Name { get; private set; }

        #region Read

        #region Sync

        public IList<Log<T>> Read<T>(DateTime date) where T : class
        {
            var result = _elasticClient.Search<Log<T>>(s =>
               s.Index(nameof(Log).ToLower())
               .From(0)
               .Size(10000)
               .Query(q => q
               .DateRange(b => b
                       .Field(field => field.LogDate)
                       .GreaterThanOrEquals(date.GenerateStartDate())
                       .LessThanOrEquals(date.GenerateEndDate())))
               .Sort(s => s.Descending(f => f.LogDate)));

            if (!result.IsValid)
                throw new Exception(result.OriginalException.Message);

            return result.Documents.ToList();
        }

        public IList<Log<T>> Read<T>(DateTime date, string level) where T : class
        {
            var result = _elasticClient.Search<Log<T>>(s => s
        .Index(nameof(Log).ToLower())
        .From(0)
       .Size(10000)
        .Query(q => q
         .Bool(b => b
             .Must(mu => mu
                 .Match(m => m
                     .Field(f => f.Level)
                     .Query(level)))
             .Filter(fi => fi
                 .DateRange(r => r
                     .Field(f => f.LogDate)
                     .GreaterThanOrEquals(date.GenerateStartDate())
                     .LessThan(date.GenerateEndDate())))))
                .Sort(p => p
                .Descending(f => f.LogDate)));
            if (!result.IsValid)
                throw new Exception(result.OriginalException.Message);

            return result.Documents.ToList();
        }

        public IList<Log<T>> Read<T>(string category, DateTime date) where T : class
        {
            var result = _elasticClient.Search<Log<T>>(s => s
         .Index(nameof(Log).ToLower())
         .From(0)
         .Size(10000)
         .Query(q => q
          .Bool(b => b
              .Must(mu => mu
                  .Match(m => m
                      .Field(f => f.Category)
                      .Query(category)))
              .Filter(fi => fi
                  .DateRange(r => r
                      .Field(f => f.LogDate)
                      .GreaterThanOrEquals(date.GenerateStartDate())
                      .LessThan(date.GenerateEndDate())))))
                 .Sort(p => p
                 .Descending(f => f.LogDate)));
            if (!result.IsValid)
                throw new Exception(result.OriginalException.Message);

            return result.Documents.ToList();
        }

        public IList<Log<T>> Read<T>(DateTime fromDate, DateTime toDate) where T : class
        {
            var result = _elasticClient.Search<Log<T>>(s =>
           s.Index(nameof(Log).ToLower())
           .From(0)
           .Size(10000)
           .Query(q => q
           .DateRange(b => b
                   .Field(field => field.LogDate)
                   .GreaterThanOrEquals(fromDate.GenerateStartDate())
                   .LessThanOrEquals(toDate.GenerateEndDate())))
           .Sort(s => s.Descending(f => f.LogDate)));

            if (!result.IsValid)
                throw new Exception(result.OriginalException.Message);

            return result.Documents.ToList();
        }

        public IList<Log<T>> Read<T>(DateTime fromDate, DateTime toDate, string level) where T : class
        {
            var result = _elasticClient.Search<Log<T>>(s => s
               .Index(nameof(Log).ToLower())
                .From(0)
                .Size(10000)
               .Query(q => q
                .Bool(b => b
                    .Must(mu => mu
                        .Match(m => m
                            .Field(f => f.Level)
                            .Query(level)))
                    .Filter(fi => fi
                        .DateRange(r => r
                            .Field(f => f.LogDate)
                            .GreaterThanOrEquals(fromDate.GenerateStartDate())
                            .LessThan(toDate.GenerateEndDate())))))
                       .Sort(p => p
                       .Descending(f => f.LogDate)));

            if (!result.IsValid)
                throw new Exception(result.OriginalException.Message);

            return result.Documents.ToList();
        }

        #endregion Sync

        #region Async

        public async Task<IList<Log<T>>> ReadAsync<T>(DateTime date) where T : class
        {
            var result = await _elasticClient.SearchAsync<Log<T>>(s =>
               s.Index(nameof(Log).ToLower())
               .From(0)
               .Size(10000)
               .Query(q => q
               .DateRange(b => b
                       .Field(field => field.LogDate)
                       .GreaterThanOrEquals(date.GenerateStartDate())
                       .LessThanOrEquals(date.GenerateEndDate())))
               .Sort(s => s.Descending(f => f.LogDate)));

            if (!result.IsValid)
                throw new Exception(result.OriginalException.Message);

            return result.Documents.ToList();
        }

        public async Task<IList<Log<T>>> ReadAsync<T>(DateTime date, string level) where T : class
        {
            var result = await _elasticClient.SearchAsync<Log<T>>(s => s
                     .Index(nameof(Log).ToLower())
                     .From(0)
                    .Size(10000)
                     .Query(q => q
                      .Bool(b => b
                          .Must(mu => mu
                              .Match(m => m
                                  .Field(f => f.Level)
                                  .Query(level)))
                          .Filter(fi => fi
                              .DateRange(r => r
                                  .Field(f => f.LogDate)
                                  .GreaterThanOrEquals(date.GenerateStartDate())
                                  .LessThan(date.GenerateEndDate())))))
                             .Sort(p => p
                             .Descending(f => f.LogDate)));
            if (!result.IsValid)
                throw new Exception(result.OriginalException.Message);

            return result.Documents.ToList();
        }

        public async Task<IList<Log<T>>> ReadAsync<T>(string category, DateTime date) where T : class
        {
            var result = await _elasticClient.SearchAsync<Log<T>>(s => s
                     .Index(nameof(Log).ToLower())
                     .From(0)
                     .Size(10000)
                     .Query(q => q
                      .Bool(b => b
                          .Must(mu => mu
                              .Match(m => m
                                  .Field(f => f.Category)
                                  .Query(category)))
                          .Filter(fi => fi
                              .DateRange(r => r
                                  .Field(f => f.LogDate)
                                  .GreaterThanOrEquals(date.GenerateStartDate())
                                  .LessThan(date.GenerateEndDate())))))
                             .Sort(p => p
                             .Descending(f => f.LogDate)));
            if (!result.IsValid)
                throw new Exception(result.OriginalException.Message);

            return result.Documents.ToList();
        }

        public async Task<IList<Log<T>>> ReadAsync<T>(DateTime fromDate, DateTime toDate) where T : class
        {
            var result = await _elasticClient.SearchAsync<Log<T>>(s =>
                    s.Index(nameof(Log).ToLower())
                    .From(0)
                    .Size(10000)
                    .Query(q => q
                    .DateRange(b => b
                            .Field(field => field.LogDate)
                            .GreaterThanOrEquals(fromDate.GenerateStartDate())
                            .LessThanOrEquals(toDate.GenerateEndDate())))
                    .Sort(s => s.Descending(f => f.LogDate)));

            if (!result.IsValid)
                throw new Exception(result.OriginalException.Message);

            return result.Documents.ToList();
        }

        public async Task<IList<Log<T>>> ReadAsync<T>(DateTime fromDate, DateTime toDate, string level) where T : class
        {
            var result = await _elasticClient.SearchAsync<Log<T>>(s => s
                       .Index(nameof(Log).ToLower())
                        .From(0)
                        .Size(10000)
                       .Query(q => q
                        .Bool(b => b
                            .Must(mu => mu
                                .Match(m => m
                                    .Field(f => f.Level)
                                    .Query(level)))
                            .Filter(fi => fi
                                .DateRange(r => r
                                    .Field(f => f.LogDate)
                                    .GreaterThanOrEquals(fromDate.GenerateStartDate())
                                    .LessThan(toDate.GenerateEndDate())))))
                               .Sort(p => p
                               .Descending(f => f.LogDate)));

            if (!result.IsValid)
                throw new Exception(result.OriginalException.Message);

            return result.Documents.ToList();
        }

        #endregion Async

        #endregion Read

        #region Write

        public void Write<T>(ILog<T> log) where T : class
        {
            if (log.Level.IsLevelEnabled(_context.MinimumLevel, _elasticSearchLogConfig.Level))
            {
                var result = _elasticClient.Index(log, i => i
                  .Index(nameof(Log).ToLower())
                  .Refresh(Elasticsearch.Net.Refresh.True));
                if (!result.IsValid)
                    throw new Exception(result.ServerError.Status.ToString());
            }
        }

        public async Task WriteAsync<T>(ILog<T> log) where T : class
        {
            if (log.Level.IsLevelEnabled(_context.MinimumLevel, _elasticSearchLogConfig.Level))
            {
                var result = await _elasticClient.IndexAsync(log, i => i
                   .Index(nameof(Log).ToLower())
                   .Refresh(Elasticsearch.Net.Refresh.True));
                if (!result.IsValid)
                    throw new Exception(result.ServerError.Status.ToString());
            }
        }

        #endregion Write
    }
}