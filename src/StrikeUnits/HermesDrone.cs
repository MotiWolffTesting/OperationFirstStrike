using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Utils;

namespace OperationFirstStrike
{
    public class HermesDrone : IStrikeUnit
    {
        public string Name { get; set; }
        public int Ammo { get; set; }
        public int Fuel { get; set; }

        public HermesDrone(string name =  "Hermes Drone", int initialAmmo = 4, int initialFuel = 80)
        {
            Name = name;
            Ammo = initialAmmo;
            Fuel = initialFuel;
        }

        public bool CanStrike(string targetType)
        {
            return targetType == "OpenArea" || targetType == "Vehicle";
        }

        public void PerformStrike(Terrorist target, IntelligenceMessage intel)
        {
            if (Ammo > 0 && Fuel >= 15)
            {
                target.IsAlive = false;
                Ammo--;
                Fuel -= 15;
            }
        }
    }
}
