using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Utils;

namespace OperationFirstStrike.Managers
{
    // Manages the collection and operations related to terrorist targets
    public class TerroristManager
    {
        // Internal list storing all known terrorists
        private readonly List<Terrorist> _terrorists = new();

        // Adds a new terrorist to the tracking system
        public void Add(Terrorist terrorist)
        {
            _terrorists.Add(terrorist);
        }

        // Returns a copy of all known terrorists
        public List<Terrorist> GetAll()
        {
            return new List<Terrorist>(_terrorists);
        }

        // Calculates a threat score based on the terrorist's weapons and rank
        // Higher rank terrorists with more dangerous weapons get higher scores
        public int GetWeaponScore(Terrorist terrorist)
        {
            return terrorist.Weapons.Sum(w => WeaponScoreRegistry.GetScore(w) * terrorist.Rank);
        }

        // Filters the list to return only terrorists that are still alive
        public List<Terrorist> GetAliveTarget(List<Terrorist> terrorists)
        {
            return terrorists.Where(t => t.IsAlive).ToList();
        }

        // Identifies the most dangerous terrorist based on weapon score
        // Returns null if no alive terrorists are found
        public Terrorist? GetMostDangerousTerrorist(List<Terrorist> terrorists)
        {
            return GetAliveTarget(terrorists)
                .OrderByDescending(t => GetWeaponScore(t))
                .FirstOrDefault();
        }
    }
}