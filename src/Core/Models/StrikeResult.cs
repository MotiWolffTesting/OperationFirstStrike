using OperationFirstStrike.Core.Interfaces;

namespace OperationFirstStrike.Core.Models
{
    public class StrikeResult
    {
        public bool Success { get; set; }
        public bool TargetEliminated { get; set; }
        public bool CollateralDamage { get; set; }
        public string? PostStrikeIntel { get; set; }
        public int FuelConsumed { get; set; }
        public int AmmoUsed { get; set; }
    }
}