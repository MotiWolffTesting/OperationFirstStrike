namespace OperationFirstStrike.Core.Models
{
    // Represents a terrorist target in the system
    public class Terrorist
    {
        // The name or alias of the terrorist
        public string Name { get; set; } = string.Empty;

        // The rank/importance level of the terrorist (higher number = higher priority)
        public int Rank { get; set; }

        // Current status of the terrorist
        public bool IsAlive { get; set; } = true;

        // List of weapons known to be in possession of the terrorist
        public List<string> Weapons { get; set; } = new();

        // Returns a string representation of the terrorist in the format "Name (Rank X)"
        public override string ToString()
        {
            return $"{Name} (Rank {Rank})";
        }
    }
}