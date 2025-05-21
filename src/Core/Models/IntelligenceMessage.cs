namespace OperationFirstStrike.Core.Models
{
    public class IntelligenceMessage
    {
        public Terrorist Target { get; set; }
        public string Location { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}