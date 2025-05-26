namespace OperationFirstStrike.Services
{
    // Manages the simulation's time progression and provides time-related functionality
    public class SimulationTimeService
    {
        // The starting point of the simulation
        private DateTime _simulationStartTime;
        // The current time within the simulation
        private DateTime _currentSimulationTime;

        // Current simulation time accessible to other components
        public DateTime CurrentTime => _currentSimulationTime;
        // Total time elapsed since simulation start
        public TimeSpan ElapsedTime => _currentSimulationTime - _simulationStartTime;

        // Initializes the simulation time service with current time as start time
        public SimulationTimeService()
        {
            _simulationStartTime = DateTime.Now;
            _currentSimulationTime = _simulationStartTime;
        }

        // Advances the simulation time by the specified duration
        // Notifies subscribers through OnTimeAdvanced event
        public void AdvanceTime(TimeSpan timeToAdvance)
        {
            _currentSimulationTime = _currentSimulationTime.Add(timeToAdvance);

            OnTimeAdvanced?.Invoke(timeToAdvance);
        }

        // Event triggered when simulation time advances
        // Subscribers can use this to update time-dependent systems
        public event Action<TimeSpan>? OnTimeAdvanced;
    }
}