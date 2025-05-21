using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.Core.Interfaces
{
    public interface IIntelligenceAnalyzer
    {
        Terrorist GetMostDangerousTerrorist(List<Terrorist> terrorists);
    }
}