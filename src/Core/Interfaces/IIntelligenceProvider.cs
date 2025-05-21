using OperationFirstStrike.Core.Models;


namespace OperationFirstStrike.Core.Interfaces
{
    public interface IIntelligenceProvider
    {
        IntelligenceMessage Generate(Terrorist terrorist);
    }
}