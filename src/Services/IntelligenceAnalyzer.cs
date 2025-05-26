using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Managers;

namespace OperationFirstStrike.Services
{
    // Analyzes intelligence data to identify high-priority targets
    public class IntelligenceAnalyzer : IIntelligenceAnalyzer
    {
        // Manages terrorist data and threat assessment
        private readonly TerroristManager _terroristManager;

        // Initializes the analyzer with required dependencies
        public IntelligenceAnalyzer(TerroristManager terroristManager)
        {
            _terroristManager = terroristManager;
        }

        // Identifies the most dangerous terrorist from a list of targets
        // Returns the terrorist with the highest threat level
        public Terrorist GetMostDangerousTerrorist(List<Terrorist> terrorists)
        {
            return _terroristManager.GetMostDangerousTerrorist(terrorists)!;
        }
    }
}