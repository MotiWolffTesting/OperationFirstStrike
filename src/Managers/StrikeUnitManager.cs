using System.Runtime.CompilerServices;
using OperationFirstStrike.Core.Interfaces;

namespace OperationFirstStrike.Managers
{
    public class StrikeUnitManager
    {
        private readonly List<IStrikeUnit> _units = new();
        public void RegisterUnit(IStrikeUnit unit) => _units.Add(unit);

        public List<IStrikeUnit> GetAvailableUnits(string targetType) => _units.Where(u => u.CanStrike(targetType) && u.Ammo > 0 && u.Fuel > 0).ToList();

        public List<IStrikeUnit> GetAllUnits() => new List<IStrikeUnit>(_units);
    }
}



