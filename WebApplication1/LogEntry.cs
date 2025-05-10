namespace WebApplication1
{
    public class LogEntry
    {
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public string Level { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
    }
}