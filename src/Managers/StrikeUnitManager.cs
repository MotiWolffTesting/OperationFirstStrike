using System.Runtime.CompilerServices;
using OperationFirstStrike.Core.Interfaces;

namespace OperationFirstStrike.Managers
{
    // Manages the collection and availability of strike units
    public class StrikeUnitManager
    {
        // Internal storage of all registered strike units
        private readonly List<IStrikeUnit> _units = new();

        // Registers a new strike unit with the system
        public void RegisterUnit(IStrikeUnit unit) => _units.Add(unit);

        // Returns units that are available for a specific target type
        // Units must have sufficient ammo and fuel to be considered available
        public List<IStrikeUnit> GetAvailableUnits(string targetType) =>
            _units.Where(u => u.CanStrike(targetType) && u.Ammo > 0 && u.Fuel > 0).ToList();

        // Returns a copy of all registered strike units
        public List<IStrikeUnit> GetAllUnits() => new List<IStrikeUnit>(_units);
    }
}



