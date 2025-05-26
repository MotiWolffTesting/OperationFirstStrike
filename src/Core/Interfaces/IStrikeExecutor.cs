using OperationFirstStrike.Core.Models;

// Defines capabilities for components that execute military strikes against terrorist targets.
// This interface represents the final action layer in the operational chain.
namespace OperationFirstStrike.Core.Interfaces
{
    // Interface for services that execute strike operations against terrorist targets.
    // Implementations should handle the tactical execution of strikes based on intelligence data
    // and the capabilities of the strike unit assigned to the mission.
    public interface IStrikeExecutor
    {
        // Executes a military strike against a terrorist target using the specified strike unit
        // and based on provided intelligence data.
        // Returns true if the strike was successful, false otherwise.
        // Success criteria may include target elimination, minimal collateral damage,
        // and mission objectives achieved.
        bool ExecuteStrike(IStrikeUnit unit, Terrorist target, IntelligenceMessage intel);
    }
}
