using OperationFirstStrike;
using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Managers;
using OperationFirstStrike.Presentation;
using OperationFirstStrike.Services;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            var console = new ConsoleDisplayManager();
            var dataInitializer = new DataInitializationService();


            var terroristManager = new TerroristManager();
            var intelManager = new IntelligenceManager();
            var strikeUnitManager = new StrikeUnitManager();
            var historyWriter = new StrikeHistoryWriter();


            console.ShowMessage("Generating terrorist database...", ConsoleColor.Yellow);
            var terrorists = dataInitializer.CreateRandomTerrorists(8);
            foreach (var terrorist in terrorists)
            {
                terroristManager.Add(terrorist);
            }

            console.ShowMessage("Gathering intelligence reports...", ConsoleColor.Yellow);
            var intelMessages = dataInitializer.GenerateIntelligenceReports(terrorists, 15);
            foreach (var message in intelMessages)
            {
                intelManager.Add(message);
            }


            strikeUnitManager.RegisterUnit(new F16FighterJet());
            strikeUnitManager.RegisterUnit(new HermesDrone());
            strikeUnitManager.RegisterUnit(new M109Artillery());


            var strikeService = new StrikeCoordinationService(strikeUnitManager);
            var userService = new UserInteractionService(console);
            var analysisService = new IntelligenceAnalysisService();


            var terroristDisplay = new TerroristDisplay();
            var intelDisplay = new IntelligenceDisplay();
            var strikeUnitDisplay = new StrikeUnitDisplay();
            var strikeHistoryDisplay = new StrikeHistoryDisplay();


            var menuController = new MenuController(
                terroristManager,
                intelManager,
                null,
                historyWriter,
                intelDisplay,
                terroristDisplay,
                strikeUnitDisplay,
                strikeHistoryDisplay
            );


            var simulation = new SimulationManager(
                menuController,
                terroristManager,
                intelManager,
                strikeService,
                userService,
                analysisService,
                console,
                historyWriter
            );

            simulation.Initialize();
            simulation.Run();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Fatal error: {ex.Message}");
            Console.ResetColor();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}