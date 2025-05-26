using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Managers;

namespace OperationFirstStrike.Services
{
    // Initializes and generates test data for the simulation
    public class DataInitializationService
    {
        // Random number generator for creating varied test data
        private readonly Random _random = new();
        // List of predefined terrorist names for simulation
        private readonly string[] _names = { "Yahya Sinwar", "Mohammed Deif", "Ismail Haniyeh", "Khaled Meshaal", "Ahmed Jabari", "Marwan Issa", "Mohammed Sinwar", "Rawhi Mushtaha" };
        // List of available weapons for terrorists
        private readonly string[] _weapons = { "knife", "gun", "m16", "ak47" };

        // Creates a list of random terrorists with unique names and random attributes
        // Parameters:
        // - count: Number of terrorists to create (default: 8)
        public List<Terrorist> CreateRandomTerrorists(int count = 8)
        {
            var terrorists = new List<Terrorist>();
            var usedNames = new HashSet<string>();

            for (int i = 0; i < count; i++)
            {
                string name;
                do
                {
                    name = _names[_random.Next(_names.Length)];
                } while (usedNames.Contains(name));

                usedNames.Add(name);

                var terrorist = new Terrorist
                {
                    Name = name,
                    Rank = _random.Next(1, 6), // 1-5 as per requirements
                    Weapons = GenerateRandomWeapons()
                };

                terrorists.Add(terrorist);
            }

            return terrorists;
        }

        // Generates a random list of weapons for a terrorist
        // Returns 1-3 random weapons from the available weapons list
        private List<string> GenerateRandomWeapons()
        {
            var weaponCount = _random.Next(1, 4); // 1-3 weapons
            var selectedWeapons = new HashSet<string>();

            while (selectedWeapons.Count < weaponCount)
            {
                selectedWeapons.Add(_weapons[_random.Next(_weapons.Length)]);
            }

            return selectedWeapons.ToList();
        }

        // Generates random intelligence reports for the given terrorists
        // Parameters:
        // - terrorists: List of terrorists to generate reports for
        // - count: Number of reports to generate (default: 15)
        public List<IntelligenceMessage> GenerateIntelligenceReports(List<Terrorist> terrorists, int count = 15)
        {
            var messages = new List<IntelligenceMessage>();
            var locations = new[] { "home", "in a car", "outside" };

            for (int i = 0; i < count; i++)
            {
                var terrorist = terrorists[_random.Next(terrorists.Count)];
                var message = new IntelligenceMessage
                {
                    Target = terrorist,
                    Location = locations[_random.Next(locations.Length)],
                    Timestamp = DateTime.Now.AddMinutes(-_random.Next(0, 1440)) // Random time within last 24 hours
                };

                messages.Add(message);
            }

            return messages;
        }
    }
}