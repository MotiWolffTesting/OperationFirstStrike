using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Managers;
using OperationFirstStrike.Presentation;
using OperationFirstStrike.Services;

namespace OperationFirstStrike.Managers
{
    // Main simulation controller that orchestrates the entire operation
    public class SimulationManager
    {
        // UI and display components
        private readonly MenuController _menu;
        private readonly ConsoleDisplayManager _console;

        // Core managers for different aspects of the system
        private readonly TerroristManager _terroristManager;
        private readonly IntelligenceManager _intelManager;

        // Services for business logic
        private readonly StrikeCoordinationService _strikeService;
        private readonly UserInteractionService _userService;
        private readonly IntelligenceAnalysisService _analysisService;
        private readonly StrikeHistoryWriter _historyWriter;

        // Initializes the simulation with all required dependencies
        public SimulationManager(
            MenuController menu,
            TerroristManager terroristManager,
            IntelligenceManager intelManager,
            StrikeCoordinationService strikeService,
            UserInteractionService userService,
            IntelligenceAnalysisService analysisService,
            ConsoleDisplayManager console,
            StrikeHistoryWriter historyWriter)
        {
            _menu = menu;
            _terroristManager = terroristManager;
            _intelManager = intelManager;
            _strikeService = strikeService;
            _userService = userService;
            _analysisService = analysisService;
            _console = console;
            _historyWriter = historyWriter;
        }

        // Sets up the initial simulation environment
        public void Initialize()
        {
            _console.ShowTitle("IDF OPERATION – FIRST STRIKE");
            _console.ShowMessage("Initializing simulation environment...", ConsoleColor.Yellow);
            Thread.Sleep(1000);
        }

        // Main simulation loop that handles user interaction and menu navigation
        public void Run()
        {
            bool running = true;

            while (running)
            {
                _menu.ShowMenu();
                Console.WriteLine("\nChoose an option: ");
                var input = Console.ReadLine()!;

                try
                {
                    switch (input)
                    {
                        case "1":
                            ShowTerroristInformation();
                            break;
                        case "2":
                            ShowIntelligenceAnalysis();
                            break;
                        case "3":
                            _menu.StrikeUnitDisplay.ShowStrikeUnits(_strikeService.GetAllUnits());
                            break;
                        case "4":
                            var history = _historyWriter.GetAllReports();
                            _menu.StrikeHistoryDisplay.ShowStrikeHistory(history);
                            break;
                        case "5":
                            ShowTargetPrioritization();
                            break;
                        case "6":
                            ConductStrike();
                            break;
                        case "7":
                            _console.ShowMessage("Exiting simulation. Stay safe, soldier.", ConsoleColor.Red);
                            running = false;
                            break;
                        default:
                            _console.ShowMessage("Invalid input. Try again.", ConsoleColor.DarkYellow);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    _console.ShowMessage($"An error occurred: {ex.Message}", ConsoleColor.Red);
                }
            }
        }

        // Displays information about all known terrorists and their threat levels
        private void ShowTerroristInformation()
        {
            var terrorists = _terroristManager.GetAll();
            var weaponScores = terrorists.ToDictionary(t => t, t => _terroristManager.GetWeaponScore(t));
            _menu.TerroristDisplay.ShowTerroristDetails(terrorists, weaponScores);
        }

        // Shows analysis of intelligence data and identifies patterns
        private void ShowIntelligenceAnalysis()
        {
            var allIntel = _intelManager.GetAllIntel();
            _menu.IntelligenceDisplay.ShowIntelligenceSummary(allIntel);

            // Show terrorist with most reports
            var topTerrorist = _analysisService.GetTerroristWithMostReports(allIntel);
            if (topTerrorist != null)
            {
                var reportCount = _analysisService.GetReportCount(allIntel, topTerrorist);
                _console.ShowMessage($"\nTerrorist with most intelligence reports: {topTerrorist.Name} ({reportCount} reports)", ConsoleColor.Cyan);
            }
        }

        // Displays prioritized targets based on threat level and intelligence
        private void ShowTargetPrioritization()
        {
            var terrorists = _terroristManager.GetAll();
            var mostDangerous = _terroristManager.GetMostDangerousTerrorist(terrorists);

            if (mostDangerous != null)
            {
                var weaponScore = _terroristManager.GetWeaponScore(mostDangerous);
                var latestIntel = _analysisService.GetLatestReports(_intelManager.GetAllIntel(), mostDangerous);
                _menu.TerroristDisplay.ShowMostDangerousTerrorist(mostDangerous, weaponScore, latestIntel);
            }
            else
            {
                _console.ShowMessage("No terrorists available for targeting.", ConsoleColor.Yellow);
            }
        }

        // Handles the strike operation process from target selection to execution
        private void ConductStrike()
        {
            var terrorists = _terroristManager.GetAll();
            var aliveTerrorists = terrorists.Where(t => t.IsAlive).ToList();

            var selectedTarget = _userService.SelectTarget(aliveTerrorists);
            if (selectedTarget == null) return;

            // Generate intelligence and execute strike
            var generator = new IntelligenceGenerator();
            var intel = generator.Generate(selectedTarget);

            _console.ShowMessage($"\nTargeting {selectedTarget.Name} at {intel.Location}...", ConsoleColor.Yellow);
            Thread.Sleep(1500);

            var (unit, success) = _strikeService.ExecuteStrike(intel);

            // Record the strike
            if (success && unit != null)
            {
                var report = new StrikeReport
                {
                    Unit = unit,
                    Target = selectedTarget,
                    Intel = intel,
                    Success = success,
                    Timestamp = DateTime.Now
                };
                _historyWriter.Record(report);
            }

            _userService.ShowStrikeResult(unit, selectedTarget, intel, success);
        }
    }
}