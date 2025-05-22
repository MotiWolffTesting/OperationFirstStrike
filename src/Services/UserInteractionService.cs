using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Presentation;

namespace OperationFirstStrike.Services
{
    public class UserInteractionService
    {
        private readonly ConsoleDisplayManager _console;

        public UserInteractionService(ConsoleDisplayManager console)
        {
            _console = console;
        }

        public Terrorist? SelectTarget(List<Terrorist> availableTargets)
        {
            if (!availableTargets.Any())
            {
                _console.ShowMessage("No terrorists available to target. All threats have been eliminated.", ConsoleColor.Yellow);
                return null;
            }

            _console.ShowTitle("SELECT TARGET");
            for (int i = 0; i < availableTargets.Count; i++)
            {
                var terrorist = availableTargets[i];
                Console.WriteLine($"{i + 1}. {terrorist.Name} (Rank: {terrorist.Rank}, Weapons: {string.Join(", ", terrorist.Weapons)})");
            }

            Console.Write("\nSelect target number: ");
            if (!int.TryParse(Console.ReadLine(), out int targetIndex) || targetIndex < 1 || targetIndex > availableTargets.Count)
            {
                _console.ShowMessage("Invalid selection. Operation aborted.", ConsoleColor.Red);
                return null;
            }

            return availableTargets[targetIndex - 1];
        }

        public void ShowStrikeResult(IStrikeUnit? unit, Terrorist target, IntelligenceMessage intel, bool success)
        {
            if (success && unit != null)
            {
                _console.ShowMessage($"STRIKE SUCCESSFUL: {unit.Name} eliminated {target.Name} at {intel.Location}", ConsoleColor.Green);
            }
            else
            {
                _console.ShowMessage($"STRIKE FAILED: No suitable strike unit available for target at {intel.Location}", ConsoleColor.Red);
            }

            _console.ShowMessage("\nPress Enter to continue...", ConsoleColor.Gray);
            Console.ReadLine();
        }
    }
}