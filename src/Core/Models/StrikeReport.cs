using OperationFirstStrike.Core.Interfaces;

namespace OperationFirstStrike.Core.Models
{
    public class StrikeReport
    {
        public IStrikeUnit? Unit { get; set; }
        public Terrorist Target { get; set; } = null!;
        public IntelligenceMessage Intel { get; set; } = null!;
        public bool Success { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
