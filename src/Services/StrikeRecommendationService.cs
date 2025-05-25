using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Utils;

namespace OperationFirstStrike.Services
{
    public class StrikeRecommendation
    {
        public IStrikeUnit? RecommendedUnit { get; set; }
        public string? Reasoning { get; set; } = string.Empty;
        public float EffectivenessScore { get; set; }
        public List<string> Alternatives { get; set; } = new();
    }

    public class StrikeRecommendationService
    {
        public StrikeRecommendation GetRecommendation(IntelligenceMessage intel, List<IStrikeUnit> availableUnits)
        {
            var recommendation = new StrikeRecommendation();
            var targetType = LocationTargetTypeMapper.GetTargetType(intel.Location);

            var suitableUnits = availableUnits
                .Where(u => u.CanStrike(targetType) && !u.IsOnCooldown && !u.NeedsRefueling && u.Ammo > 0)
                .ToList();

            if (!suitableUnits.Any())
            {
                recommendation.Reasoning = "No suitable units available. Check cooldowns, fuel, and ammo.";
                return recommendation;
            }

            // Score each unit based on effectivenss
            var scoredUnit = suitableUnits.Select(unit => new
            {
                Unit = unit,
                Score = CalculateEffectiveness(unit, targetType, intel)
            }).OrderByDescending(x => x.Score).ToList();

            var best = scoredUnit.First();
            recommendation.RecommendedUnit = best.Unit;
            recommendation.EffectivenessScore = best.Score;
            recommendation.Reasoning = $"Best match: {best.Unit.Name} (effectiveness: {best.Score:F1}/10)";

            recommendation.Alternatives = scoredUnit.Skip(1).Take(2)
                .Select(x => $"{x.Unit.Name} (effectiveness: {x.Score:F1}/10)")
                .ToList();

            return recommendation;
        }

        private float CalculateEffectiveness(IStrikeUnit unit, string targetType, IntelligenceMessage intel)
        {
            float score = 0;

            // Base effectivness for target type
            if (unit.CanStrike(targetType)) score += 5;

            // Ammo availability
            score += Math.Min(unit.Ammo / 2f, 2);

            // Fuel status
            if (unit.Fuel > unit.FuelThreshold * 2) score += 1.5f;
            else if (unit.Fuel > unit.FuelThreshold) score += 1;

            // Intelligence confidence score
            score += intel.ConfidenceScore / 100f;

            // Unit-specific bonuses
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

