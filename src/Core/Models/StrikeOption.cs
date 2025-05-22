using OperationFirstStrike;

namespace OperationFirstStrike.Core.Models
{
    public class StrikeOption
    {
        public string UnitName { get; set; } = string.Empty;
        public string TargetName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public bool Success { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;

        // Convert from StrikeReport to StrikeOption for display purposes
        public static StrikeOption FromStrikeReport(StrikeReport report)
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
