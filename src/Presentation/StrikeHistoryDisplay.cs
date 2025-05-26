using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.Presentation
{
    // Handles the display of strike operation history in the console
    public class StrikeHistoryDisplay
    {
        // Displays a chronological list of all strike operations
        // Shows timestamp, unit, target, location, and success status for each strike
        public void ShowStrikeHistory(List<StrikeReport> reports)
        {
            Console.WriteLine("\n[Strike History]");
            if (reports.Count == 0)
            {
                Console.WriteLine("No strikes have been conducted yet.");
                return;
            }
            
            foreach (var report in reports)
            {
                string unitName = report.Unit?.Name ?? "Unknown Unit";
                string targetName = report.Target?.Name ?? "Unknown Target";
                string location = report.Intel?.Location ?? "Unknown Location";
                
                Console.WriteLine($"- {report.Timestamp}: {unitName} struck {targetName} at {location} | Success: {report.Success}");
            }
        }
    }
}
