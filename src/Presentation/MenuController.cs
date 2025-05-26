using OperationFirstStrike.Managers;
using OperationFirstStrike.Services;

namespace OperationFirstStrike.Presentation
{
    // Controls the main menu and coordinates all display components
    public class MenuController
    {
        // Core managers for data access
        private readonly TerroristManager _terroristManager;
        private readonly IntelligenceManager _intelManager;
        private readonly StrikeCoordinator _coordinator;
        private readonly StrikeHistoryWriter _historyWriter;

        // Display components for different types of information
        private readonly IntelligenceDisplay _intelDisplay;
        private readonly TerroristDisplay _terroristDisplay;
        private readonly StrikeUnitDisplay _strikeDisplay;
        private readonly StrikeHistoryDisplay _strikeHistoryDisplay;

        // Public access to display components
        public IntelligenceDisplay IntelligenceDisplay => _intelDisplay;
        public TerroristDisplay TerroristDisplay => _terroristDisplay;
        public StrikeUnitDisplay StrikeUnitDisplay => _strikeDisplay;
        public StrikeHistoryDisplay StrikeHistoryDisplay => _strikeHistoryDisplay;

        // Initializes the menu controller with all required dependencies
        public MenuController(
            TerroristManager terroristManager,
            IntelligenceManager intelManager,
            StrikeCoordinator coordinator,
            StrikeHistoryWriter historyWriter,
            IntelligenceDisplay intelDisplay,
            TerroristDisplay terroristDisplay,
            StrikeUnitDisplay strikeDisplay,
            StrikeHistoryDisplay strikeHistoryDisplay)
        {
            _terroristManager = terroristManager;
            _intelManager = intelManager;
            _coordinator = coordinator;
            _historyWriter = historyWriter;
            _intelDisplay = intelDisplay;
            _terroristDisplay = terroristDisplay;
            _strikeDisplay = strikeDisplay;
            _strikeHistoryDisplay = strikeHistoryDisplay;
        }

        // Displays the main menu options to the user
        public void ShowMenu()
        {
            Console.WriteLine("\n[Main Menu]");
            Console.WriteLine("1. View Terrorist Information");
            Console.WriteLine("2. Intel Analysis");
            Console.WriteLine("3. View Strike Units");
            Console.WriteLine("4. View Strike History");
            Console.WriteLine("5. Target Prioritization");
            Console.WriteLine("6. Conduct Strike");
            Console.WriteLine("7. Exit");
        }
    }
}