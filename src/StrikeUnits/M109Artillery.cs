using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.StrikeUnits
{
    // Represents an M109 self-propelled artillery unit for long-range strikes
    public class M109Artillery : IStrikeUnit
    {
        // Name of the artillery unit
        public string Name { get; set; }
        // Current ammunition count
        public int Ammo { get; set; }
        // Current fuel level
        public int Fuel { get; set; }
        // Maximum fuel capacity
        public int MaxFuel { get; } = 50;
        // Fuel level at which refueling is recommended
        public int FuelThreshold { get; } = 15;
        // Timestamp of the last strike operation
        public DateTime LastStrikeTime { get; set; } = DateTime.MinValue;
        // Time required between operations
        public TimeSpan CooldownDuration { get; } = TimeSpan.FromMinutes(20);
        // Whether the unit is currently in cooldown period
        public bool IsOnCooldown { get; private set; }

        // Initializes a new artillery unit with specified name and initial resources
        public M109Artillery(string name = "M109 Artillery", int initialAmmo = 40, int initialFuel = 50)
        {
            Name = name;
            Ammo = initialAmmo;
            Fuel = initialFuel;
        }

        // Determines if the unit can strike a specific target type
        // Artillery units can only strike open areas
        public bool CanStrike(string targetType) => targetType == "OpenArea";

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
        // Includes collateral damage assessment and potential intelligence discovery
        public StrikeResult PerformEnhancedStrike(Terrorist target, IntelligenceMessage intel)
        {
            var result = new StrikeResult();
            var random = new Random();

            if (Ammo > 0 && Fuel >= 10 && !IsOnCooldown)
            {
                Ammo--;
                Fuel -= 10;
                result.FuelConsumed = 10;
                result.AmmoUsed = 1;
                LastStrikeTime = DateTime.Now;

                bool strikeHits = random.Next(1, 101) <= intel.ConfidenceScore;

                if (strikeHits)
                {
                    target.IsAlive = false;
                    result.Success = true;
                    result.TargetEliminated = true;
                    result.CollateralDamage = random.Next(1, 101) <= 30; // 30% chance (artillery)

                    if (random.Next(1, 101) <= 20)
                    {
                        result.PostStrikeIntel = "Artillery strike revealed underground tunnels";
                    }
                }
                else
                {
                    result.Success = false;
                    result.CollateralDamage = random.Next(1, 101) <= 40; // Higher for missed artillery
                }
            }

            return result;
        }
    }
}