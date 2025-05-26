using OperationFirstStrike.Core.Models;

// Defines capabilities for intelligence analysis operations within the system.
// Used for processing terrorist threat data and identifying high-priority targets.
namespace OperationFirstStrike.Core.Interfaces
{
    // Interface for components that analyze intelligence data and identify threats.
    // Implementations should apply threat assessment algorithms to terrorist profiles.
    public interface IIntelligenceAnalyzer
    {
        // Analyzes a list of terrorists and returns the one deemed most dangerous
        // based on implemented threat assessment criteria (e.g., threat level, 
        // location, past activities, weapons access).
        Terrorist GetMostDangerousTerrorist(List<Terrorist> terrorists);
    }
}