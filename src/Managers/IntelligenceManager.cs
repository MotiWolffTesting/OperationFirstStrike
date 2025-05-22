using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.Managers
{
    public class IntelligenceManager
    {
        private readonly List<IntelligenceMessage> _intelMessages = new();
        public void Add(IntelligenceMessage message)
        {
            _intelMessages.Add(message);
        }

        public List<IntelligenceMessage> GetAll()
        {
            return new List<IntelligenceMessage>(_intelMessages);
        }

        public List<IntelligenceMessage> GetAllIntel()
        {
            return GetAll();
        }

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