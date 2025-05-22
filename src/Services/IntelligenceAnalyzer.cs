using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Managers;

namespace OperationFirstStrike.Services
{
    public class IntelligenceAnalyzer : IIntelligenceAnalyzer
    {
        private readonly TerroristManager _terroristManager;

        public IntelligenceAnalyzer(TerroristManager terroristManager)
        {
            _terroristManager = terroristManager;
        }

        public Terrorist GetMostDangerousTerrorist(List<Terrorist> terrorists)
        {
            return _terroristManager.GetMostDangerousTerrorist(terrorists)!;
        }
    }
}