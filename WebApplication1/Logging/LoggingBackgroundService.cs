
using MongoDB.Driver;

namespace WebApplication1.Logging
{
    public class LoggingBackgroundService : BackgroundService
    {
        private readonly ILoggingQueue _queue;
        private readonly IMongoCollection<LogEntry> _logCollection;
        private readonly string _fallbackFile = "log_fallback.txt";

        public LoggingBackgroundService(ILoggingQueue queue)
        {
            _queue = queue;

            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("LoggingDb");
            _logCollection = database.GetCollection<LogEntry>("Logs");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var log in ((LoggingQueue)_queue).Reader.ReadAllAsync(stoppingToken))
            {
                try
                {
                    await _logCollection.InsertOneAsync(log, cancellationToken: stoppingToken);
                }
                catch
                {
                    try
                    {
                        await File.AppendAllTextAsync(_fallbackFile,
                            $"{log.TimeStamp} | {log.Level} | {log.Message} | {log.Source}{Environment.NewLine}", stoppingToken);
                    }
                    catch
                    {
                        
                    }
                }
            }
        }
    }

}
