using OperationFirstStrike.Core.Interfaces;

namespace OperationFirstStrike.Core.Models
{
    // Represents a detailed report of a completed strike operation
    public class StrikeReport
    {
        // The military unit that executed the strike
        public IStrikeUnit? Unit { get; set; }

        // The terrorist target of the strike
        public Terrorist Target { get; set; } = null!;

        // The intelligence that led to this strike
        public IntelligenceMessage Intel { get; set; } = null!;

        // Whether the strike was successful
        public bool Success { get; set; }

        // When the strike was executed
        public DateTime Timestamp { get; set; }
    }
}
