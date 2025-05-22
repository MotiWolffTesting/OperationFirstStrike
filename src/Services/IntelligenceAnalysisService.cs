using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.Services
{
    public class IntelligenceAnalysisService
    {
        public Terrorist? GetTerroristWithMostReports(List<IntelligenceMessage> messages)
        {
            return messages
                .GroupBy(m => m.Target)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key;
        }

        public int GetReportCount(List<IntelligenceMessage> messages, Terrorist terrorist)
        {
            return messages.Count(m => m.Target == terrorist);
        }

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
