namespace OperationFirstStrike.Utils
{
    // Registry that maintains a scoring system for different weapons
    // Used to assess the threat level of terrorists based on their weapons
    public static class WeaponScoreRegistry
    {
        // Dictionary mapping weapon names to their threat scores
        // Higher scores indicate more dangerous weapons
        private static readonly Dictionary<string, int> WeaponScore = new()
        {
            {"knife", 1},    // Basic melee weapon
            {"gun", 2},      // Standard firearm
            {"m16", 3},      // Military-grade assault rifle
            {"ak47", 3}      // Military-grade assault rifle
        };

        // Retrieves the threat score for a given weapon
        // Returns a default score of 5 for unknown weapons
        public static int GetScore(string weapon)
        {
            return WeaponScore.TryGetValue(weapon, out var score) ? score : 5;
        }
    }
}