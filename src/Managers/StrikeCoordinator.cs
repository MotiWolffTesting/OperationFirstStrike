using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Utils;

namespace OperationFirstStrike.Managers
{
    public class StrikeCoordinator
    {
        private readonly StrikeUnitManager _strikeUnitManager;
        public StrikeCoordinator(StrikeUnitManager strikeUnitManager)
        {
            _strikeUnitManager = strikeUnitManager;
        }

        public (IStrikeUnit? Unit, bool Success) CoordinateStrike(IntelligenceMessage intel)
        {
            string targetType = LocationTargetTypeMapper.GetTargetType(intel.Location);
            var candidates = _strikeUnitManager.GetAvailableUnits(targetType);

            if (!candidates.Any())
                return (null, false);

            var selectedUnit = candidates.First();
            selectedUnit.PerformStrike(intel.Target, intel);
            return (selectedUnit, true);
        }
    }
}