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
        public StrikeResult PerformStrike(Terrorist target, IntelligenceMessage intel)
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

                bool captureAttempt = random.Next(1, 101) <= 50;
                bool operationSuccess = random.Next(1, 101) <= intel.ConfidenceScore;

                if (operationSuccess)
                {
                    result.Success = true;
                    if (captureAttempt)
                    {
                        target.IsAlive = false;
                        result.TargetEliminated = false;
                        result.PostStrikeIntel = $"Target {target.Name} captured alive. Interrogation resulted in valuable intel.";
                    }
                    else
                    {
                        target.IsAlive = false;
                        result.TargetEliminated = true;
                    }

                    result.CollateralDamage = random.Next(1, 101) <= 5;
                }
                else
                {
                    result.Success = false;
                    result.CollateralDamage = random.Next(1, 101) <= 10;
                }
            }
            return result;
        }

        public void Refuel()
        {
            Fuel = MaxFuel;
            Ammo += 2;
        }
    }
}