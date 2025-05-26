using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.StrikeUnits
{
    // Represents a Hermes UAV (Unmanned Aerial Vehicle) for precision strikes and surveillance
    public class HermesDrone : IStrikeUnit
    {
        // Name of the drone unit
        public string Name { get; set; }
        // Current ammunition count
        public int Ammo { get; set; }
        // Current fuel level
        public int Fuel { get; set; }
        // Maximum fuel capacity
        public int MaxFuel { get; } = 80;
        // Fuel level at which refueling is recommended
        public int FuelThreshold { get; } = 20;
        // Timestamp of the last strike operation
        public DateTime LastStrikeTime { get; set; } = DateTime.MinValue;
        // Time required between operations
        public TimeSpan CooldownDuration { get; } = TimeSpan.FromMinutes(10);
        // Whether the unit is currently in cooldown period
        public bool IsOnCooldown { get; private set; }

        // Initializes a new drone unit with specified name and initial resources
        public HermesDrone(string name = "Hermes Drone", int initialAmmo = 3, int initialFuel = 80)
        {
            Name = name;
            Ammo = initialAmmo;
            Fuel = initialFuel;
        }

        // Determines if the unit can strike a specific target type
        // Drones can strike persons and vehicles
        public bool CanStrike(string targetType) => targetType == "Person" || targetType == "Vehicle";

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

            if (Ammo > 0 && Fuel >= 15 && !IsOnCooldown)
            {
                Ammo--;
                Fuel -= 15;
                result.FuelConsumed = 15;
                result.AmmoUsed = 1;
                LastStrikeTime = DateTime.Now;

                bool strikeHits = random.Next(1, 101) <= intel.ConfidenceScore;

                if (strikeHits)
                {
                    target.IsAlive = false;
                    result.Success = true;
                    result.TargetEliminated = true;
                    result.CollateralDamage = random.Next(1, 101) <= 10; // 10% chance

                    if (random.Next(1, 101) <= 40)
                    {
                        result.PostStrikeIntel = "Drone surveillance captured additional intel";
                    }
                }
                else
                {
                    result.Success = false;
                    result.CollateralDamage = random.Next(1, 101) <= 15;
                }
            }

            return result;
        }
    }
}