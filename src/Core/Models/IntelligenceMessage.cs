namespace OperationFirstStrike.Core.Models
{
    public class IntelligenceMessage
    {
        public Terrorist Target { get; set; } = null!;
        public string Location { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}