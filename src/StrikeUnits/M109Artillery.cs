using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Utils;

namespace OperationFirstStrike
{
    public class M109Artillery : IStrikeUnit
    {
        public string Name { get; set; }
        public int Ammo { get; set; }
        public int Fuel { get; set; }

        public int MaxFuel => throw new NotImplementedException();

        public int FuelThreshold => throw new NotImplementedException();

        public DateTime LastStrikeTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public TimeSpan CooldownDuration => throw new NotImplementedException();

        public M109Artillery(string name = "M109 Artillery", int initialAmmo = 10, int initialFuel = 50)
        {
            Name = name;
            Ammo = initialAmmo;
            Fuel = initialFuel;
        }

        public bool CanStrike(string targetType)
        {
            return targetType == "OpenArea";
        }

        public void PerformStrike(Terrorist target, IntelligenceMessage intel)
        {
            if (Ammo > 0 && Fuel >= 10)
            {
                target.IsAlive = false;
                Ammo--;
                Fuel -= 10;
            }
        }

        public void Refuel()
        {
            throw new NotImplementedException();
        }

        StrikeResult IStrikeUnit.PerformStrike(Terrorist target, IntelligenceMessage intel)
        {
            throw new NotImplementedException();
        }
    }
}