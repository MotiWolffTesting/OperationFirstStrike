using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Managers;
using OperationFirstStrike.Utils;

namespace OperationFirstStrike.Services
{
    // Coordinates and executes strike operations using available strike units
    public class StrikeCoordinationService
    {
        // Manages the pool of available strike units
        private readonly StrikeUnitManager _strikeUnitManager;

        // Initializes the service with required dependencies
        public StrikeCoordinationService(StrikeUnitManager strikeUnitManager)
        {
            _strikeUnitManager = strikeUnitManager;
        }

        // Executes a strike operation based on intelligence
        // Returns the unit that performed the strike and whether it was successful
        public (IStrikeUnit? Unit, bool Success) ExecuteStrike(IntelligenceMessage intel)
        {
            // Determine the type of target based on location
            string targetType = LocationTargetTypeMapper.GetTargetType(intel.Location);
            var candidates = _strikeUnitManager.GetAvailableUnits(targetType);

            if (!candidates.Any())
                return (null, false);

            // Select and deploy the first available unit
            var selectedUnit = candidates.First();
            selectedUnit.PerformStrike(intel.Target, intel);

            return (selectedUnit, true);
        }

        // Returns all available strike units
        internal List<IStrikeUnit> GetAllUnits()
        {
            throw new NotImplementedException();
        }
    }
}