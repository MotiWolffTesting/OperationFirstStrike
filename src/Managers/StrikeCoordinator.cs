using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Services;
using OperationFirstStrike.Utils;

namespace OperationFirstStrike.Managers
{
    public class StrikeCoordinator
    {
        private readonly StrikeUnitManager _strikeUnitManager;
        private readonly StrikeHistoryWriter? _historyWriter;
        private readonly List<StrikeReport> _reports = new();

        public StrikeCoordinator(StrikeUnitManager strikeUnitManager, StrikeHistoryWriter? historyWriter = null)
        {
            _strikeUnitManager = strikeUnitManager;
            _historyWriter = historyWriter;
        }

        public (IStrikeUnit? Unit, bool Success) CoordinateStrike(IntelligenceMessage intel)
        {
            string targetType = LocationTargetTypeMapper.GetTargetType(intel.Location);
            var candidates = _strikeUnitManager.GetAvailableUnits(targetType);

            if (!candidates.Any())
                return (null, false);

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

        public List<StrikeReport> GetHistory()
        {
            return new List<StrikeReport>(_reports);
        }

        public List<IStrikeUnit> GetAllUnits()
        {
            return _strikeUnitManager.GetAllUnits();
        }
    }
}