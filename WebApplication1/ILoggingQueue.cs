namespace WebApplication1
{
    public interface ILoggingQueue
    {
        void Enqueue(LogEntry entry);
    }
}
