using OperationFirstStrike.Managers;
using OperationFirstStrike.Services;

namespace OperationFirstStrike.Presentation
{
    public class MenuController
    {
        private readonly TerroristManager _terroristManager;
        private readonly IntelligenceManager _intelManager;
        private readonly StrikeCoordinator _coordinator;
        private readonly StrikeHistoryWriter _historyWriter;

        private readonly IntelligenceDisplay _intelDisplay;
        private readonly TerroristDisplay _terroristDisplay;
        private readonly StrikeUnitDisplay _strikeDisplay;
        private readonly StrikeHistoryDisplay _strikeHistoryDisplay;

        public IntelligenceDisplay IntelligenceDisplay => _intelDisplay;
        public TerroristDisplay TerroristDisplay => _terroristDisplay;
        public StrikeUnitDisplay StrikeUnitDisplay => _strikeDisplay;
        public StrikeHistoryDisplay StrikeHistoryDisplay => _strikeHistoryDisplay;

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

        public void ShowMenu()
        {
            Console.WriteLine("\n[Main Menu]");
            Console.WriteLine("1. View Terrorist Info");
            Console.WriteLine("2. View Intelligence Reports");
            Console.WriteLine("3. View Strike Units");
            Console.WriteLine("4. View Strike History");
            Console.WriteLine("5. Conduct Strike");
            Console.WriteLine("6. Exit");
        }
    }
}