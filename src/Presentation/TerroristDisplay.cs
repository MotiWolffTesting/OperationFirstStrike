using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Managers;

namespace OperationFirstStrike.Presentation
{
    public class TerroristDisplay
    {
        public void ShowTerroristDetails(List<Terrorist> terrorists, Dictionary<Terrorist, int> weaponScore)
        {
            Console.WriteLine("\n- Terrorists - ");
            foreach (var terrorist in terrorists)
            {
                string status = terrorist.IsAlive ? "Alive" : "Killed as a hobby";
                int score = weaponScore.ContainsKey(terrorist) ? weaponScore[terrorist] : 0;
                Console.WriteLine($"{terrorist.Name} Details: Rank - {terrorist.Rank}, Weapons - {string.Join(", ", terrorist.Weapons)}, Score - {score}, Status - {status}.");
            }
        }

        public void ShowMostDangeroursTerrorist(Terrorist terrorist, int weaponScore, List<IntelligenceMessage> latestIntel)
        {
            Console.WriteLine("\n=== Most Dangerous Terrorist ===");
            Console.WriteLine($"Name: {terrorist.Name}");
            Console.WriteLine($"Rank: {terrorist.Rank}");
            Console.WriteLine($"Quality Score: {weaponScore}");
            Console.WriteLine($"Weapons: {string.Join(", ", terrorist.Weapons)}");

            if (latestIntel.Any())
            {
                var latest = latestIntel.First();
                Console.WriteLine($"Last Known Location: {latest.Location} (as of {latest.Timestamp:yyyy-MM-dd HH:mm})");
            }
            else
            {
                Console.WriteLine("Last Known Location: No recent intelligence available");
            }
        }
    }
}