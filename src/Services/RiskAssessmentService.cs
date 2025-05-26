using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Core.Interfaces;

namespace OperationFirstStrike.Services
{
    // Represents the level of risk associated with a strike operation
    public enum RiskLevel
    {
        Low,
        Medium,
        High,
        Critical
    }

    // Contains the results of a risk assessment for a strike operation
    public class RiskAssessment
    {
        // The overall risk level of the operation
        public RiskLevel Level { get; set; }
        // Numerical risk score (0-100)
        public int Score { get; set; }
        // Explanation of the risk assessment
        public string Reasoning { get; set; } = string.Empty;
        // List of factors contributing to the risk score
        public List<string> Factors { get; set; } = new();
    }

    // Evaluates the risk associated with potential strike operations
    public class RiskAssessmentService
    {
        // Calculates the risk assessment for a strike operation
        // Takes into account target danger, location, intelligence confidence, and unit readiness
        public RiskAssessment CalculateRisk(Terrorist target, IntelligenceMessage intel, IStrikeUnit unit, int terroristDangerScore)
        {
            var assessment = new RiskAssessment();
            var factors = new List<string>();
            int riskScore = 0;

            // Terrorist danger level (0-40 points)
            int dangerPoints = Math.Min(terroristDangerScore / 2, 40);
            riskScore += dangerPoints;
            factors.Add($"Target danger level: {dangerPoints}/40");

            // Location exposure (0-20 points)
            int locationRisk = intel.Location switch
            {
                "home" => 5,      // Lower risk for stationary targets
                "outside" => 15,  // Higher risk for mobile targets
                "in a car" => 10, // Medium risk for moving vehicles
                _ => 10
            };
            riskScore += locationRisk;
            factors.Add($"Location exposure ({intel.Location}): {locationRisk}/20");

            // Intelligence confidence (0-20 points, inverse)
            int intelRisk = 20 - (intel.ConfidenceScore / 5);
            riskScore += intelRisk;
            factors.Add($"Intel uncertainty (confidence {intel.ConfidenceScore}%): {intelRisk}/20");

            // Unit readiness (0-20 points)
            int readinessRisk = 0;
            if (unit.Ammo <= 1) readinessRisk += 10;
            if (unit.NeedsRefueling) readinessRisk += 10;
            riskScore += readinessRisk;
            factors.Add($"Unit readiness: {readinessRisk}/20");

            assessment.Score = riskScore;
            assessment.Factors = factors;

            // Determine risk level based on total score
            assessment.Level = riskScore switch
            {
                <= 25 => RiskLevel.Low,
                <= 50 => RiskLevel.Medium,
                <= 75 => RiskLevel.High,
                _ => RiskLevel.Critical
            };

            assessment.Reasoning = $"Overall risk score: {riskScore}/100 - {assessment.Level} risk";
            return assessment;
        }
    }
}