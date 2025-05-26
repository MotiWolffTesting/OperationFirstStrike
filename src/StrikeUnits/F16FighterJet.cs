using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.StrikeUnits
{
    // Represents an F-16 fighter jet for high-precision aerial strikes against buildings
    public class F16FighterJet : IStrikeUnit
    {
        // Name of the fighter jet unit
        public string Name { get; set; }
        // Current ammunition count
        public int Ammo { get; set; }
        // Current fuel level
        public int Fuel { get; set; }
        // Maximum fuel capacity
        public int MaxFuel { get; } = 100;
        // Fuel level at which refueling is recommended
        public int FuelThreshold { get; } = 30;
        // Timestamp of the last strike operation
        public DateTime LastStrikeTime { get; set; } = DateTime.MinValue;
        // Time required between operations
        public TimeSpan CooldownDuration { get; } = TimeSpan.FromMinutes(15);
        // Whether the unit is currently in cooldown period
        public bool IsOnCooldown { get; private set; }

        // Initializes a new fighter jet unit with specified name and initial resources
        public F16FighterJet(string name = "F-16 Fighter Jet", int initialAmmo = 8, int initialFuel = 100)
        {
            Name = name;
            Ammo = initialAmmo;
            Fuel = initialFuel;
        }

        // Determines if the unit can strike a specific target type
        // F-16s are specialized for striking buildings
        public bool CanStrike(string targetType) => targetType == "Building";

        // Refuels the unit
        public void Refuel()
        {
            Fuel = MaxFuel;
        }

        // Legacy method for performing strikes (maintained for compatibility)
        public void PerformStrike(Terrorist target, IntelligenceMessage intel)
        {
            var result = PerformEnhancedStrike(target, intel);
        }

        // Enhanced strike operation with detailed results
        // Includes collateral damage assessment and potential intelligence gathering
        public StrikeResult PerformEnhancedStrike(Terrorist target, IntelligenceMessage intel)
        {
            var result = new StrikeResult();
            var random = new Random();

            if (Ammo > 0 && Fuel >= 25 && !IsOnCooldown)
            {
                // Consume resources
                Ammo--;
                Fuel -= 25;
                result.FuelConsumed = 25;
                result.AmmoUsed = 1;
                LastStrikeTime = DateTime.Now;

                // Determine success based on intel confidence
                bool strikeHits = random.Next(1, 101) <= intel.ConfidenceScore;

                if (strikeHits)
                {
                    target.IsAlive = false;
                    result.Success = true;
                    result.TargetEliminated = true;

                    // Collateral damage chance (lower for precision strikes)
                    result.CollateralDamage = random.Next(1, 101) <= 15; // 15% chance

                    // Post-strike intel
                    if (random.Next(1, 101) <= 30) // 30% chance
                    {
                        result.PostStrikeIntel = "Secondary targets identified in blast radius";
                    }
                }
                else
                {
                    result.Success = false;
                    result.CollateralDamage = random.Next(1, 101) <= 25; // Higher chance if missed
                }
            }

            return result;
        }
    }
}