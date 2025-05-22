using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Utils;

namespace OperationFirstStrike.Managers
{
    public class TerroristManager
    {
        public int GetWeaponScore(Terrorist terrorist)
        {
            return terrorist.Weapons.Sum(w => WeaponScoreRegistry.GetScore(w) * terrorist.Rank);
        }

        public List<Terrorist> GetAliveTarget(List<Terrorist> terrorists)
        {
            return terrorists.Where(t => t.IsAlive).ToList();
        }

        public Terrorist? GetMostDangerousTerrorist(List<Terrorist> terrorists)
        {
            return GetAliveTarget(terrorists)
                .OrderByDescending(t => GetWeaponScore(t))
                .FirstOrDefault();
        }
    }
}