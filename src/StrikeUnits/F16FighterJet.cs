using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.StrikeUnits
{
    public class F16FighterJet : IStrikeUnit
    {
        public string Name { get; set; }
        public int Ammo { get; set; }
        public int Fuel { get; set; }
        public int MaxFuel { get; } = 100;
        public int FuelThreshold { get; } = 30;
        public DateTime LastStrikeTime { get; set; } = DateTime.MinValue;
        public TimeSpan CooldownDuration { get; } = TimeSpan.FromMinutes(15);
        public bool IsOnCooldown { get; private set; }

        public F16FighterJet(string name = "F-16 Fighter Jet", int initialAmmo = 8, int initialFuel = 100)
        {
            Name = name;
            Ammo = initialAmmo;
            Fuel = initialFuel; // ✅ FIXED
        }

        public bool CanStrike(string targetType) => targetType == "Building";

        public void Refuel()
        {
            Fuel = MaxFuel;
        }

        // OLD METHOD - for backwards compatibility
        public void PerformStrike(Terrorist target, IntelligenceMessage intel)
        {
            var result = PerformEnhancedStrike(target, intel);
            // Old method just does the basic strike without returning details
        }

        // NEW METHOD - Enhanced strike with detailed results
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