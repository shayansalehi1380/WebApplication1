using System.Threading.Channels;

namespace WebApplication1
{
    public class LoggingQueue : ILoggingQueue
    {
        private readonly Channel<LogEntry> _channel = Channel.CreateUnbounded<LogEntry>();

        public ChannelReader<LogEntry> Reader => _channel.Reader;

        public void Enqueue(LogEntry entry)
        {
            _channel.Writer.TryWrite(entry);
        }
    }
}
