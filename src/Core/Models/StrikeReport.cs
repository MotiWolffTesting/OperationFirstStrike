namespace OperationFirstStrike.Core.Models
{
    public class StrikeReport
    {
        public IStrikeUnit Unit { get; set; }
        public Terrorist Target { get; set; }
        public IntelligenceMessage Intel { get; set; }
        public bool Success { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
