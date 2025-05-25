using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.StrikeUnits
{
    public class HermesDrone : IStrikeUnit
    {
        public string Name { get; set; }
        public int Ammo { get; set; }
        public int Fuel { get; set; }
        public int MaxFuel { get; } = 80;
        public int FuelThreshold { get; } = 20;
        public DateTime LastStrikeTime { get; set; } = DateTime.MinValue;
        public TimeSpan CooldownDuration { get; } = TimeSpan.FromMinutes(10);
        public bool IsOnCooldown { get; private set; }

        public HermesDrone(string name = "Hermes Drone", int initialAmmo = 3, int initialFuel = 80)
        {
            Name = name;
            Ammo = initialAmmo;
            Fuel = initialFuel;
        }

        public bool CanStrike(string targetType) => targetType == "Person" || targetType == "Vehicle";

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