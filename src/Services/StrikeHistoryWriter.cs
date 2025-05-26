using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.Services
{
    // Manages the history of strike operations, recording and providing access to strike reports
    public class StrikeHistoryWriter
    {
        // Internal storage for all strike reports
        private readonly List<StrikeReport> _reports = new();

        // Records a new strike report in the history
        public void Record(StrikeReport report)
        {
            _reports.Add(report);
        }

        // Returns a copy of all recorded strike reports
        public List<StrikeReport> GetAllReports()
        {
            return new List<StrikeReport>(_reports);
        }

        // Returns the count of successful strikes
        public int GetSuccessCount()
        {
            return _reports.Count(r => r.Success);
        }

        // Returns the count of failed strikes
        public int GetFailureCount()
        {
            return _reports.Count(r => !r.Success);
        }

        // Clears all recorded strike history
        public void ClearHistory()
        {
            _reports.Clear();
        }
    }
}