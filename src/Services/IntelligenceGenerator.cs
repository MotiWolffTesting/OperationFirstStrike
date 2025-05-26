using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.Services
{
    // Generates intelligence messages with simulated location data
    public class IntelligenceGenerator
    {
        // Random number generator for creating varied intelligence data
        private readonly Random _random = new();
        // List of possible locations where terrorists might be found
        private readonly string[] _locations = { "home", "in a car", "outside" };

        // Generates a new intelligence message for a specific terrorist
        // Creates a message with random location and current timestamp
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