using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.StrikeUnits
{
    // Represents a ground-based special forces unit capable of both capture and elimination operations
    public class GroundUnit : IStrikeUnit
    {
        // Name of the ground unit (e.g., "Sayeret Matkal", "Shayetet 13")
        public string Name { get; set; }
        // Current ammunition count
        public int Ammo { get; set; }
        // Current fuel level
        public int Fuel { get; set; }
        // Maximum fuel capacity
        public int MaxFuel { get; } = 50;
        // Fuel level at which refueling is recommended
        public int FuelThreshold { get; } = 10;
        // Timestamp of the last strike operation
        public DateTime LastStrikeTime { get; set; } = DateTime.MinValue;
        // Time required between operations
        public TimeSpan CooldownDuration { get; } = TimeSpan.FromMinutes(30); // Longer cooldown
        // Whether the unit is currently in cooldown period
        public bool IsOnCooldown { get; private set; }

        // Initializes a new ground unit with specified name and initial resources
        public GroundUnit(string name = "Special Forces Unit", int initialAmmo = 3, int initialFuel = 50)
        {
            Name = name;
            Ammo = initialAmmo;
            Fuel = initialFuel;
        }

        // Determines if the unit can strike a specific target type
        // Ground units can strike buildings and vehicles
        public bool CanStrike(string targetType) => targetType == "Building" || targetType == "Vehicle";

        // Refuels the unit and resupplies ammunition
        public void Refuel()
        {
            Fuel = MaxFuel;
            Ammo += 2; // Resupply ammo during "refuel"
        }

        // Legacy method for performing strikes (maintained for compatibility)
        public void PerformStrike(Terrorist target, IntelligenceMessage intel)
        {
            var result = PerformEnhancedStrike(target, intel);
            // Old method just does the strike without returning details
        }

        // Enhanced strike operation with detailed results
        // Includes capture attempts and collateral damage assessment
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

                // Ground units have capture option (50% capture, 50% eliminate)
                bool captureAttempt = random.Next(1, 101) <= 50;
                bool operationSuccess = random.Next(1, 101) <= intel.ConfidenceScore;

                if (operationSuccess)
                {
                    result.Success = true;
                    if (captureAttempt)
                    {
                        target.IsAlive = false; // Captured (removed from active threats)
                        result.TargetEliminated = false;
                        result.PostStrikeIntel = $"Target {target.Name} captured alive. Interrogation yields valuable intel.";
                    }
                    else
                    {
                        target.IsAlive = false;
                        result.TargetEliminated = true;
                    }

                    result.CollateralDamage = random.Next(1, 101) <= 5; // Very low collateral
                }
                else
                {
                    result.Success = false;
                    result.CollateralDamage = random.Next(1, 101) <= 10;
                }
            }

            return result;
        }
    }
}