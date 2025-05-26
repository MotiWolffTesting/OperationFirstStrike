using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.Managers
{
    // Manages intelligence messages and provides methods to query intelligence data
    public class IntelligenceManager
    {
        // Internal storage of all intelligence messages
        private readonly List<IntelligenceMessage> _intelMessages = new();

        // Adds a new intelligence message to the system
        public void Add(IntelligenceMessage message)
        {
            _intelMessages.Add(message);
        }

        // Returns a copy of all intelligence messages
        public List<IntelligenceMessage> GetAll()
        {
            return new List<IntelligenceMessage>(_intelMessages);
        }

        // Alias for GetAll() - returns all intelligence messages
        public List<IntelligenceMessage> GetAllIntel()
        {
            return GetAll();
        }

        // Returns the 3 most recent intelligence messages for a specific target
        // Messages are ordered by timestamp (newest first)
        public List<IntelligenceMessage> GetLatestFromTarget(Terrorist target)
        {
            return _intelMessages
                .Where(m => m.Target == target)
                .OrderByDescending(t => t.Timestamp)
                .Take(3)
                .ToList();
        }
    }
}