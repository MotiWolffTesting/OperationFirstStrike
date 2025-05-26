using OperationFirstStrike.Core.Models;

// Defines capabilities for components that generate intelligence information
// about terrorist targets within the operation.
namespace OperationFirstStrike.Core.Interfaces
{
    // Interface for services that provide intelligence data about terrorists.
    // Implementations should generate actionable intelligence messages based on
    // terrorist profiles and gathered field information.
    public interface IIntelligenceProvider
    {
        // Generates an intelligence message containing critical information about a specific terrorist.
        // The message may include location data, threat assessment, recent activities,
        // and other relevant intelligence for operational planning.
        IntelligenceMessage Generate(Terrorist terrorist);
    }
}