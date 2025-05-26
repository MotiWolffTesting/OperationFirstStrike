using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Utils;

namespace OperationFirstStrike.Services
{
    // Represents a recommendation for a strike operation, including the recommended unit and alternatives
    public class StrikeRecommendation
    {
        // The strike unit recommended for the operation
        public IStrikeUnit? RecommendedUnit { get; set; }
        // Explanation of why this unit was recommended
        public string? Reasoning { get; set; } = string.Empty;
        // Calculated effectiveness score for the recommended unit (0-10)
        public float EffectivenessScore { get; set; }
        // List of alternative units that could be used
        public List<string> Alternatives { get; set; } = new();
    }

    // Provides intelligent recommendations for strike operations based on available units and intelligence
    public class StrikeRecommendationService
    {
        // Generates a strike recommendation based on intelligence and available units
        // Returns a recommendation object containing the best unit and alternatives
        public StrikeRecommendation GetRecommendation(IntelligenceMessage intel, List<IStrikeUnit> availableUnits)
        {
            var recommendation = new StrikeRecommendation();
            var targetType = LocationTargetTypeMapper.GetTargetType(intel.Location);

            // Filter units that are suitable for the target type and have necessary resources
            var suitableUnits = availableUnits
                .Where(u => u.CanStrike(targetType) && !u.IsOnCooldown && !u.NeedsRefueling && u.Ammo > 0)
                .ToList();

            if (!suitableUnits.Any())
            {
                recommendation.Reasoning = "No suitable units available. Check cooldowns, fuel, and ammo.";
                return recommendation;
            }

            // Score each unit based on effectiveness
            var scoredUnit = suitableUnits.Select(unit => new
            {
                Unit = unit,
                Score = CalculateEffectiveness(unit, targetType, intel)
            }).OrderByDescending(x => x.Score).ToList();

            // Select the best unit and prepare alternatives
            var best = scoredUnit.First();
            recommendation.RecommendedUnit = best.Unit;
            recommendation.EffectivenessScore = best.Score;
            recommendation.Reasoning = $"Best match: {best.Unit.Name} (effectiveness: {best.Score:F1}/10)";

            recommendation.Alternatives = scoredUnit.Skip(1).Take(2)
                .Select(x => $"{x.Unit.Name} (effectiveness: {x.Score:F1}/10)")
                .ToList();

            return recommendation;
        }

        // Calculates an effectiveness score (0-10) for a unit against a specific target
        // Takes into account unit capabilities, resources, and intelligence confidence
        private float CalculateEffectiveness(IStrikeUnit unit, string targetType, IntelligenceMessage intel)
        {
            float score = 0;

            // Base effectiveness for target type
            if (unit.CanStrike(targetType)) score += 5;

            // Ammo availability (up to 2 points)
            score += Math.Min(unit.Ammo / 2f, 2);

            // Fuel status (up to 1.5 points)
            if (unit.Fuel > unit.FuelThreshold * 2) score += 1.5f;
            else if (unit.Fuel > unit.FuelThreshold) score += 1;

            // Intelligence confidence score (up to 1 point)
            score += intel.ConfidenceScore / 100f;

            // Unit-specific bonuses for certain target types
            score += unit.Name switch
            {
                var name when name.Contains("F-16") && targetType == "Building" => 1,
                var name when name.Contains("Hermes") && targetType == "Vehicle" => 1,
                var name when name.Contains("M109") && targetType == "OpenArea" => 1,
                _ => 0
            };

            return score;
        }
    }
}

