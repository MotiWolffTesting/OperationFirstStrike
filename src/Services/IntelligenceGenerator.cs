using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.Services
{
    public class IntelligenceGenerator
    {
        private readonly Random _random = new();
        private readonly string[] _locations = { "home", "in a car", "outside" };

        public IntelligenceMessage Generate(Terrorist terrorist)
        {
            return new IntelligenceMessage
            {
                Target = terrorist,
                Location = _locations[_random.Next(_locations.Length)],
                Timestamp = DateTime.Now
            };
        }
    }
}