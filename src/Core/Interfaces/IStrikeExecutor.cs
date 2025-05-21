using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.Core.Interfaces
{
    public interface IStrikeExecutor
    {
        bool ExecuteStrike(IStrikeUnit unit, Terrorist target, IntelligenceMessage intel);
    }
}
