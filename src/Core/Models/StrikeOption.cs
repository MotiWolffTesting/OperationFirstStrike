using OperationFirstStrike;
using OperationFirstStrike.Core.Interfaces;

namespace OperationFirstStrike.Core.Models
{
    // Represents a potential strike operation option that can be presented to the user
    public class StrikeOption
    {
        // Name of the military unit that would execute the strike
        public string UnitName { get; set; } = string.Empty;

        // Name of the terrorist target
        public string TargetName { get; set; } = string.Empty;

        // Location where the strike would be executed
        public string Location { get; set; } = string.Empty;

        // Whether this strike option is expected to be successful
        public bool Success { get; set; }

        // When this strike option was generated
        public DateTime Timestamp { get; set; } = DateTime.Now;

        // Creates a StrikeOption from a StrikeReport for display purposes
        // This is used to convert historical strike data into a format suitable for UI presentation
        public static StrikeOption FormStrikeReport(StrikeReport report)
        {
            return new StrikeOption
            {
                UnitName = report.Unit?.Name ?? "Unknown Unit",
                TargetName = report.Target?.Name ?? "Unknown Target",
                Location = report.Intel?.Location ?? "Unknown Location",
                Success = report.Success,
                Timestamp = report.Timestamp
            };
        }
    }
}
