namespace OperationFirstStrike.Core.Models
{
    public class Terrorist
    {
        public string Name { get; set; } = string.Empty;
        public int Rank { get; set; }
        public bool IsAlive { get; set; } = true;
        public List<string> Weapons { get; set; } = new();

        public override string ToString()
        {
            return $"{Name} (Rank {Rank})";
        }
    }
}