using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Managers;

namespace OperationFirstStrike.Presentation
{
    public class TerroristDisplay
    {
        private readonly TerroristManager _manager;
        public TerroristDisplay(TerroristManager manager)
        {
            _manager = manager;
        }

        public void ShowTerroristDetails(List<Terrorist> terrorists)
        {
            Console.WriteLine("\n- Terrorists - ");
            foreach (var terrorist in terrorists)
            {
                string status = terrorist.IsAlive ? "Alive" : "Killed as a hobby";
                int score = _manager.GetWeaponScore(terrorist);
                Console.WriteLine($"{terrorist.Name} Details: Rank - {terrorist.Rank}, Weapons - {string.Join(", ", terrorist.Weapons)}, Score - {score}, {status}.");
            }
        }
    }
}