using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Utils;

namespace OperationFirstStrike
{
    public class F16FighterJet : IStrikeUnit
    {
        public string Name { get; set; }
        public int Ammo { get; set; }
        public int Fuel { get; set; }

        public F16FighterJet(string name = "F-16 Fighter Jet", int initialAmmo = 6, int initialFuel = 100)
        {
            Name = name;
            Ammo = initialAmmo;
            Fuel = initialFuel;
        }
        public bool CanStrike(string targetType)
        {
            return targetType == "Building" || targetType == "Vehicle";
        }

        public void PerformStrike(Terrorist target, IntelligenceMessage intel)
        {
            if (Ammo > 0 && Fuel >= 20)
            {
                target.IsAlive = false;
                Ammo--;
                Fuel -= 20;
            }
        }

    }
}