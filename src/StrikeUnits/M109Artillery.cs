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

        public M109Artillery(string name = "M109 Artillery", int initialAmmo = 10, int initialFuel = 50)
        {
            Name = name;
            Ammo = initialAmmo;
            Fuel = initialFuel;
        }

        public bool CanStrike(string targetType)
        {
            return targetType == "Building";
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
    }
}