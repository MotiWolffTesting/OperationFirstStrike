using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Core.Interfaces;

namespace OperationFirstStrike.Services
{
    // Tracks statistics for individual weapons used in strike operations
    public class WeaponUsageStats
    {
        // Name of the weapon system
        public string WeaponName { get; set; } = string.Empty;
        // Number of times the weapon has been used
        public int TimesUsed { get; set; }
        // Number of successful strikes with this weapon
        public int SuccessfulStrikes { get; set; }
        // Calculated success rate as a percentage
        public float SuccessRate => TimesUsed > 0 ? (float)SuccessfulStrikes / TimesUsed * 100 : 0;
    }

    // Aggregates overall mission statistics and performance metrics
    public class MissionStatistics
    {
        // Total number of strike operations conducted
        public int TotalStrikes { get; set; }
        // Number of successful strike operations
        public int SuccessfulStrikes { get; set; }
        // Number of terrorists eliminated
        public int TerroristsEliminated { get; set; }
        // Number of terrorists captured
        public int TerroristsCaptured { get; set; }
        // Number of collateral damage incidents
        public int CollateralIncidents { get; set; }
        // Overall success rate as a percentage
        public float SuccessRate => TotalStrikes > 0 ? (float)SuccessfulStrikes / TotalStrikes * 100 : 0;
        // Efficiency metric for ammunition usage
        public float AmmoEfficiency { get; set; }
    }

    // Provides analytics and statistics for strike operations and weapon usage
    public class AnalyticsService
    {
        // Collection of all strike reports for analysis
        private readonly List<StrikeReport> _allStrikes = new();
        // Statistics for each weapon system used
        private readonly Dictionary<string, WeaponUsageStats> _weaponStats = new();

        // Records a new strike operation and updates weapon statistics
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

        // Generates comprehensive mission statistics
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

        // Returns weapon usage statistics sorted by success rate
        public List<WeaponUsageStats> GetWeaponStatistics()
        {
            return _weaponStats.Values.OrderByDescending(w => w.SuccessRate).ToList();
        }

        // Searches for terrorists by name or weapon type
        public List<Terrorist> SearchTerrorists(List<Terrorist> allTerrorists, string searchTerm)
        {
            return allTerrorists.Where(t => 
                t.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                t.Weapons.Any(w => w.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            ).ToList();
        }
    }
}