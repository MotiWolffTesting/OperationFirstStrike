namespace OperationFirstStrike.Services
{
    public class SimulationTimeService
    {
        private DateTime _simulationStartTime;
        private DateTime _currentSimulationTime;

        public DateTime CurrentTime => _currentSimulationTime;
        public TimeSpan ElapsedTime => _currentSimulationTime - _simulationStartTime;

        public SimulationTimeService()
        {
            _simulationStartTime = DateTime.Now;
            _currentSimulationTime = _simulationStartTime;
        }

        public void AdvanceTime(TimeSpan timeToAdvance)
        {
            _currentSimulationTime = _currentSimulationTime.Add(timeToAdvance);

            OnTimeAdvanced?.Invoke(timeToAdvance);
        }

        public event Action<TimeSpan>? OnTimeAdvanced;
    }
}