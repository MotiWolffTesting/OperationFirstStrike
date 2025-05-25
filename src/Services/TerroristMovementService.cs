using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.Services
{
    // Service responsible for simulating terrorist movement patterns and location changes.
    // This service tracks terrorist movements and notifies subscribers when a terrorist changes location.
    public class TerroristMovementService
    {
        // Random number generator for creating unpredictable movement patterns.
        private readonly Random _random = new();
        
        // Possible locations where terrorists might be found.
        private readonly string[] _locations = { "home", "in a car", "outside", "hideout", "market", "mosque" };
        
        // Dictionary tracking when each terrorist last moved, used to determine next movement time.
        private readonly Dictionary<Terrorist, DateTime> _lastMovements = new();

        // Updates the locations of terrorists based on realistic movement patterns.
        // Each terrorist has a chance to move to a new location after a random interval (2-6 hours).
        // When a terrorist moves, subscribers are notified via the OnTerroristMoved event.
        // Parameters:
        // - terrorists: List of terrorists to update movement for
        // - timeAdvanced: Amount of simulation time that has passed since the last update
        public void UpdateTerroristLocations(List<Terrorist> terrorists, TimeSpan timeAdvanced)
        {
            // Only process terrorists who are still alive
            foreach (var terrorist in terrorists.Where(t => t.IsAlive))
            {
                // Initialize movement tracking for new terrorists
                if (!_lastMovements.ContainsKey(terrorist))
                {
                    _lastMovements[terrorist] = DateTime.Now;
                    continue;
                }

                // Calculate if it's time for this terrorist to move based on time elapsed
                // Terrorists move at variable intervals between 2-6 hours for realistic unpredictability
                var timeSinceLastMove = DateTime.Now - _lastMovements[terrorist];
                var moveInterval = TimeSpan.FromHours(_random.Next(2, 7));

                if (timeSinceLastMove >= moveInterval)
                {
                    // Generate new location intelligence by randomly selecting from possible locations
                    var newLocation = _locations[_random.Next(_locations.Length)];
                    _lastMovements[terrorist] = DateTime.Now; // Reset the movement timer
                    
                    // Notify subscribers that a terrorist has moved to trigger intelligence updates
                    OnTerroristMoved?.Invoke(terrorist, newLocation);
                }
            }
        }

        // Event triggered when a terrorist moves to a new location.
        // Subscribers can use this event to generate new intelligence or update tracking systems.
        // The event provides the terrorist object and their new location string.
        public event Action<Terrorist, string>? OnTerroristMoved;
    }
}