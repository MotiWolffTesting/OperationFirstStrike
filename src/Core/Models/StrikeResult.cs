using OperationFirstStrike.Core.Interfaces;

namespace OperationFirstStrike.Core.Models
{
    // Represents the outcome of a military strike operation
    public class StrikeResult
    {
        // Indicates whether the strike operation was successfully executed
        public bool Success { get; set; }

        // Indicates whether the primary target was eliminated
        public bool TargetEliminated { get; set; }

        // Indicates whether there was any unintended damage to non-target entities
        public bool CollateralDamage { get; set; }

        // Additional intelligence gathered after the strike operation
        public string? PostStrikeIntel { get; set; }

        // Amount of fuel consumed during the strike operation
        public int FuelConsumed { get; set; }

        // Amount of ammunition used during the strike operation
        public int AmmoUsed { get; set; }
    }
}