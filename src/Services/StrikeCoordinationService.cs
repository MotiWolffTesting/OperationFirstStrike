using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Managers;
using OperationFirstStrike.Utils;

namespace OperationFirstStrike.Services
{
    public class StrikeCoordinationService
    {
        private readonly StrikeUnitManager _strikeUnitManager;

        public StrikeCoordinationService(StrikeUnitManager strikeUnitManager)
        {
            _strikeUnitManager = strikeUnitManager;
        }

        public (IStrikeUnit? Unit, bool Success) ExecuteStrike(IntelligenceMessage intel)
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