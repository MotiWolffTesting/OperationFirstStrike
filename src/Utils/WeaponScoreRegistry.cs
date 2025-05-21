namespace OperationFirstStrike.Utils
{
    public static class WeaponScoreRegistry
    {
        private static readonly Dictionary<string, int> WeaponScore = new()
        {
            {"knife", 1},
            {"gun", 2},
            {"m16", 3},
            {"ak47", 3}
        };

        public static int GetScore(string weapon)
        {
            return WeaponScore.TryGetValue(weapon, out var score) ? score : 5;
        }
    }
}