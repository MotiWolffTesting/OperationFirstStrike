using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.Presentation
{
    public class StrikeHistoryDisplay
    {
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
