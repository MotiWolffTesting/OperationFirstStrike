using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.Services
{
    public class StrikeHistoryWriter
    {
        private readonly List<StrikeReport> _reports;
        public void Record(StrikeReport report)
        {
            _reports.Add(report);
        }

        public List<StrikeReport> GetAllReports()
        {
            return new List<StrikeReport>(_reports);
        }

        public int GetSuccessCount()
        {
            return _reports.Count(r => r.Success);
        }

        public int GetFailureCount()
        {
            return _reports.Count(r => !r.Success);
        }

        public void ClearHistory()
        {
            _reports.Clear();
        }
    }
}