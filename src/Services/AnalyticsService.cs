using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Core.Interfaces;

namespace OperationFirstStrike.Services
{
    public class WeaponUsageStats
    {
        public string WeaponName { get; set; } = string.Empty;
        public int TimesUsed { get; set; }
        public int SuccessfulStrikes { get; set; }
        public float SuccessRate => TimesUsed > 0 ? (float)SuccessfulStrikes / TimesUsed * 100 : 0;
    }

    public class MissionStatistics
    {
        public int TotalStrikes { get; set; }
        public int SuccessfulStrikes { get; set; }
        public int TerroristsEliminated { get; set; }
        public int TerroristsCaptured { get; set; }
        public int CollateralIncidents { get; set; }
        public float SuccessRate => TotalStrikes > 0 ? (float)SuccessfulStrikes / TotalStrikes * 100 : 0;
        public float AmmoEfficiency { get; set; }
    }

     public class AnalyticsService
    {
        private readonly List<StrikeReport> _allStrikes = new();
        private readonly Dictionary<string, WeaponUsageStats> _weaponStats = new();

        public void RecordStrike(StrikeReport strike)
        {
            _allStrikes.Add(strike);
            
            if (strike.Unit != null)
            {
                var weaponName = strike.Unit.Name;
                if (!_weaponStats.ContainsKey(weaponName))
                {
                    _weaponStats[weaponName] = new WeaponUsageStats { WeaponName = weaponName };
                }

                _weaponStats[weaponName].TimesUsed++;
                if (strike.Success)
                {
                    _weaponStats[weaponName].SuccessfulStrikes++;
                }
            }
        }

        public MissionStatistics GetMissionStatistics()
        {
            var stats = new MissionStatistics
            {
                TotalStrikes = _allStrikes.Count,
                SuccessfulStrikes = _allStrikes.Count(s => s.Success),
                TerroristsEliminated = _allStrikes.Count(s => s.Success && s.Target?.IsAlive == false)
            };

            return stats;
        }

        public List<WeaponUsageStats> GetWeaponStatistics()
        {
            return _weaponStats.Values.OrderByDescending(w => w.SuccessRate).ToList();
        }

        public List<Terrorist> SearchTerrorists(List<Terrorist> allTerrorists, string searchTerm)
        {
            return allTerrorists.Where(t => 
                t.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                t.Weapons.Any(w => w.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            ).ToList();
        }
    }
}