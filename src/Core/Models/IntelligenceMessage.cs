namespace OperationFirstStrike.Core.Models
{
    // Represents an intelligence report about a potential terrorist target
    public class IntelligenceMessage
    {
        // Different sources of intelligence information
        public enum IntelSource
        {
            Drone,              // Aerial surveillance using drones
            UnderCoverAgent,    // Information from undercover operatives
            CyberUnit,         // Intelligence gathered through cyber operations
            Satellite,         // Satellite imagery and surveillance
            HumanIntel,        // Information from human sources
            ElectronicSurveillance  // Signals intelligence and electronic monitoring
        }

        // The terrorist target this intelligence is about
        public Terrorist Target { get; set; } = null!;

        // The location where the target was observed
        public string Location { get; set; } = string.Empty;

        // When this intelligence was gathered
        public DateTime Timestamp { get; set; } = DateTime.Now;

        // Intelligence is considered expired after 24 hours
        public bool IsExpired => DateTime.Now.Subtract(Timestamp).TotalHours > 24;

        // Confidence level in the intelligence (0-100)
        public int ConfidenceScore { get; set; } = 50;

        // The source of this intelligence
        public IntelSource Source { get; set; } = IntelSource.Drone;

        // Intelligence is considered reliable if confidence is high and not expired
        public bool IsReliable => ConfidenceScore >= 70 && !IsExpired;
    }
}