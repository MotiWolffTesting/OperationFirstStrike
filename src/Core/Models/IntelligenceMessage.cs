namespace OperationFirstStrike.Core.Models
{
    public class IntelligenceMessage
    {
        public enum IntelSource
        {
            Drone,
            UnderCoverAgent,
            CyberUnit,
            Satellite,
            HumanIntel,
            ElectronicSurveillance
        }

        public Terrorist Target { get; set; } = null!;
        public string Location { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public bool IsExpired => DateTime.Now.Subtract(Timestamp).TotalHours > 24;
        public int ConfidenceScore { get; set; } = 50;
        public IntelSource Source { get; set; } = IntelSource.Drone;
        public bool IsReliable => ConfidenceScore >= 70 && !IsExpired;
    }
}