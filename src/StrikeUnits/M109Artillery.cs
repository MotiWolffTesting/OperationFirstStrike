using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.StrikeUnits
{
    public class M109Artillery : IStrikeUnit
    {
        public string Name { get; set; }
        public int Ammo { get; set; }
        public int Fuel { get; set; }
        public int MaxFuel { get; } = 50;
        public int FuelThreshold { get; } = 15;
        public DateTime LastStrikeTime { get; set; } = DateTime.MinValue;
        public TimeSpan CooldownDuration { get; } = TimeSpan.FromMinutes(20);
        public bool IsOnCooldown { get; private set; }

        public M109Artillery(string name = "M109 Artillery", int initialAmmo = 40, int initialFuel = 50)
        {
            Name = name;
            Ammo = initialAmmo;
            Fuel = initialFuel;
        }

        public bool CanStrike(string targetType) => targetType == "OpenArea";

        public void Refuel()
        {
            Fuel = MaxFuel;
        }

        public void PerformStrike(Terrorist target, IntelligenceMessage intel)
        {
            var result = PerformEnhancedStrike(target, intel);
        }

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