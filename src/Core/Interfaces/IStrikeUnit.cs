using OperationFirstStrike.Core.Models;

// Defines capabilities required for military units that can execute strikes against terrorist targets.
// Strike units include various assets like fighter jets, drones, artillery, and ground units,
// each with specific operational capabilities and limitations.
namespace OperationFirstStrike.Core.Interfaces
{
    // Interface for military units capable of executing strikes against terrorist targets.
    // Implementations represent different types of strike units with varying capabilities,
    // operational parameters, and tactical considerations.
    public interface IStrikeUnit
    {
        // Basic properties
        // Name of the strike unit (e.g., "F-16 Fighter Jet", "Hermes Drone")
        string Name { get; set; }
        // Current ammunition count available for strikes
        int Ammo { get; set; }
        // Current fuel level, decreases during operations
        int Fuel { get; set; }

        // Enhanced properties for fuel management
        // Maximum fuel capacity of the unit
        int MaxFuel { get; }
        // Minimum fuel level at which the unit should be refueled for safety
        int FuelThreshold { get; }

        // Cooldown system
        // Timestamp when the last strike was performed
        DateTime LastStrikeTime { get; set; }
        // Required time between consecutive strikes for this unit
        TimeSpan CooldownDuration { get; }
        // Indicates if the unit is currently in cooldown period and cannot strike
        bool IsOnCooldown => DateTime.Now.Subtract(LastStrikeTime) < CooldownDuration;

        // Refueling
        // Indicates if the unit needs refueling based on current fuel level vs threshold
        bool NeedsRefueling => Fuel < FuelThreshold;
        // Refills the unit's fuel to maximum capacity
        void Refuel();

        // Determines if this unit can execute a strike against a specific target type
        // (e.g., building, vehicle, personnel) based on capabilities and current status
        bool CanStrike(string targetType);

        // BACKWARDS COMPATIBILITY - Keep old method
        // Executes a basic strike against a terrorist target using provided intelligence
        void PerformStrike(Terrorist target, IntelligenceMessage intel);

        // NEW METHOD - Enhanced strike with detailed results
        // Executes an advanced strike operation with comprehensive result information
        // including success/failure, damage assessment, and collateral impact
        StrikeResult PerformEnhancedStrike(Terrorist target, IntelligenceMessage intel);
    }
}
