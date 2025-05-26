using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Services;
using OperationFirstStrike.Utils;

namespace OperationFirstStrike.Managers
{
    // Coordinates strike operations by selecting appropriate units and recording strike history
    public class StrikeCoordinator
    {
        // Manages available strike units
        private readonly StrikeUnitManager _strikeUnitManager;

        // Optional service for persisting strike history
        private readonly StrikeHistoryWriter? _historyWriter;

        // In-memory storage of strike reports
        private readonly List<StrikeReport> _reports = new();

        // Initializes the coordinator with required dependencies
        public StrikeCoordinator(StrikeUnitManager strikeUnitManager, StrikeHistoryWriter? historyWriter = null)
        {
            _strikeUnitManager = strikeUnitManager;
            _historyWriter = historyWriter;
        }

        // Coordinates a strike operation based on intelligence
        // Returns the unit that performed the strike and whether it was successful
        public (IStrikeUnit? Unit, bool Success) CoordinateStrike(IntelligenceMessage intel)
        {
            // Determine the type of target based on location
            string targetType = LocationTargetTypeMapper.GetTargetType(intel.Location);
            var candidates = _strikeUnitManager.GetAvailableUnits(targetType);

            if (!candidates.Any())
                return (null, false);

            // Select and deploy the first available unit
            var selectedUnit = candidates.First();
            selectedUnit.PerformStrike(intel.Target, intel);

            // Record the strike in history
            var report = new StrikeReport
            {
                Unit = selectedUnit,
                Target = intel.Target,
                Intel = intel,
                Success = true,
                Timestamp = DateTime.Now
            };

            _reports.Add(report);
            _historyWriter?.Record(report);

            return (selectedUnit, true);
        }

        // Returns a copy of all strike reports
        public List<StrikeReport> GetHistory()
        {
            return new List<StrikeReport>(_reports);
        }

        // Returns all available strike units
        public List<IStrikeUnit> GetAllUnits()
        {
            return _strikeUnitManager.GetAllUnits();
        }
    }
}