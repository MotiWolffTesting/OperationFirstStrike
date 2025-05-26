using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Managers;

namespace OperationFirstStrike.Services
{
    // Executes strike operations and generates strike reports
    public class StrikeExecutor
    {
        // Coordinates the selection and deployment of strike units
        private readonly StrikeCoordinator _coordinator;

        // Initializes the executor with required dependencies
        public StrikeExecutor(StrikeCoordinator coordinator)
        {
            _coordinator = coordinator;
        }

        // Executes a strike operation based on intelligence
        // Returns a strike report containing the operation details and outcome
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