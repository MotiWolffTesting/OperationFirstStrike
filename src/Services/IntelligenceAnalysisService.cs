using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.Services
{
    // Analyzes intelligence messages to extract meaningful insights and patterns
    public class IntelligenceAnalysisService
    {
        // Identifies the terrorist with the most intelligence reports
        // Returns the terrorist who appears most frequently in the messages
        public Terrorist? GetTerroristWithMostReports(List<IntelligenceMessage> messages)
        {
            return messages
                .GroupBy(m => m.Target)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key;
        }

        // Counts the number of intelligence reports for a specific terrorist
        // Returns the total count of reports mentioning the terrorist
        public int GetReportCount(List<IntelligenceMessage> messages, Terrorist terrorist)
        {
            return messages.Count(m => m.Target == terrorist);
        }

        // Retrieves the most recent intelligence reports for a specific terrorist
        // Parameters:
        // - messages: List of all intelligence messages
        // - terrorist: Target terrorist to find reports for
        // - count: Number of latest reports to return (default: 3)
        public List<IntelligenceMessage> GetLatestReports(List<IntelligenceMessage> messages, Terrorist terrorist, int count = 3)
        {
            return messages
                .Where(m => m.Target == terrorist)
                .OrderByDescending(m => m.Timestamp)
                .Take(count)
                .ToList();
        }
    }
}
