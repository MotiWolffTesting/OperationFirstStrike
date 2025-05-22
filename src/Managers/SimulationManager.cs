using OperationFirstStrike.Managers;
using OperationFirstStrike.Presentation;
using OperationFirstStrike.Services;

namespace OperationFirstStrike.Managers
{
    public class SimulationManager
    {
        private readonly MenuController _menu;
        private readonly TerroristManager _terroristManager;
        private readonly IntelligenceManager _intelManager;
        private readonly StrikeCoordinator _coordinator;
        private readonly ConsoleDisplayManager _console;

        public SimulationManager(
            MenuController menu,
            TerroristManager terroristManager,
            IntelligenceManager intelManager,
            StrikeCoordinator coordinator)
        {
            _menu = menu;
            _terroristManager = terroristManager;
            _intelManager = intelManager;
            _coordinator = coordinator;
            _console = new ConsoleDisplayManager();
        }

        public void Initialize()
        {
            _console.ShowTitle("IDF OPERATION – FIRST STRIKE");
            _console.ShowMessage("Initializing simulation environment...", ConsoleColor.Yellow);
            Thread.Sleep(1000);
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                _menu.ShowMenu();
                Console.WriteLine("\nChoose an option: ");
                var input = Console.ReadLine()!;

                switch (input)
                {
                    case "1":
                        _menu.TerroristDisplay.ShowTerroristDetails(_terroristManager.GetAll());
                        break;
                    case "2":
                        _menu.IntelligenceDisplay.ShowIntelligenceSummary(_intelManager.GetAllIntel());
                        break;
                    case "3":
                        _menu.StrikeUnitDisplay.ShowStrikeUnits(_coordinator.GetAllUnits());
                        break;
                    case "4":
                        var history = _coordinator.GetHistory();
                        _menu.StrikeHistoryDisplay.ShowStrikeHistory(history);
                        break;
                    case "5":
                        ConductStrike();
                        break;
                    case "6":
                        _console.ShowMessage("Exiting simulation. Stay safe, soldier.", ConsoleColor.Red);
                        running = false;
                        break;
                    default:
                        _console.ShowMessage("Invalid input. Try again.", ConsoleColor.DarkYellow);
                        break;
                }
            }
        }
        
        private void ConductStrike()
        {
            // Step 1: Get the list of available terrorists
            var terrorists = _terroristManager.GetAll();
            var aliveTerrorists = terrorists.Where(t => t.IsAlive).ToList();
            
            if (!aliveTerrorists.Any())
            {
                _console.ShowMessage("No terrorists available to target. All threats have been eliminated.", ConsoleColor.Yellow);
                return;
            }
            
            // Step 2: Display terrorists and ask user to select a target
            _console.ShowTitle("SELECT TARGET");
            for (int i = 0; i < aliveTerrorists.Count; i++)
            {
                var terrorist = aliveTerrorists[i];
                Console.WriteLine($"{i + 1}. {terrorist.Name} (Rank: {terrorist.Rank}, Weapons: {string.Join(", ", terrorist.Weapons)})");
            }
            
            Console.Write("\nSelect target number: ");
            if (!int.TryParse(Console.ReadLine(), out int targetIndex) || targetIndex < 1 || targetIndex > aliveTerrorists.Count)
            {
                _console.ShowMessage("Invalid selection. Operation aborted.", ConsoleColor.Red);
                return;
            }
            
            // Step 3: Generate intelligence for the selected target
            var target = aliveTerrorists[targetIndex - 1];
            var generator = new IntelligenceGenerator();
            var intel = generator.Generate(target);
            
            // Step 4: Execute the strike
            _console.ShowMessage($"\nTargeting {target.Name} at {intel.Location}...", ConsoleColor.Yellow);
            Thread.Sleep(1500);
            
            var (unit, success) = _coordinator.CoordinateStrike(intel);
            
            // Step 5: Report the outcome
            if (success)
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