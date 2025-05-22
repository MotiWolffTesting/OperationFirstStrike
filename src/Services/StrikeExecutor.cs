using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Managers;

namespace OperationFirstStrike.Services
{
    public class StrikeExecutor
    {
        private readonly StrikeCoordinator _coordinator;
        public StrikeExecutor(StrikeCoordinator coordinator)
        {
            _coordinator = coordinator;
        }

        public StrikeReport Execute(IntelligenceMessage intel)
        {
            var (unit, success) = _coordinator.CoordinateStrike(intel);

            return new StrikeReport
            {
                Unit = unit,
                Target = intel.Target,
                Timestamp = DateTime.Now,
                Success = success
            };
        }
    }
}