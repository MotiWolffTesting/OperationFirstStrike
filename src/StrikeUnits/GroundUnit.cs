using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.StrikeUnits
{
    public class GroundUnit : IStrikeUnit
    {
        public string Name { get; set; }
        public int Ammo { get; set; }
        public int Fuel { get; set; }
        public int MaxFuel { get; } = 50;
        public int FuelThreshold { get; } = 10;
        public DateTime LastStrikeTime { get; set; } = DateTime.MinValue;
        public TimeSpan CooldownDuration { get; } = TimeSpan.FromMinutes(30); // Longer cooldown
        public bool IsOnCooldown { get; private set; }

        public GroundUnit(string name = "Special Forces Unit", int initialAmmo = 3, int initialFuel = 50)
        {
            Name = name;
            Ammo = initialAmmo;
            Fuel = initialFuel;
        }

        public bool CanStrike(string targetType) => targetType == "Building" || targetType == "Vehicle";

        public void Refuel()
        {
            Fuel = MaxFuel;
            Ammo += 2; // Resupply ammo during "refuel"
        }

        // ✅ FIXED: OLD METHOD - Returns void (for backwards compatibility)
        public void PerformStrike(Terrorist target, IntelligenceMessage intel)
        {
            var result = PerformEnhancedStrike(target, intel);
            // Old method just does the strike without returning details
        }

        // ✅ FIXED: NEW METHOD - Enhanced strike with detailed results
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