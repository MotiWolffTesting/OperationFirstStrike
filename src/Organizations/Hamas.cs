using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike
{
    // Represents the Hamas terrorist organization and its members
    public class Hamas
    {
        // When the organization was established
        public DateTime DateOfFormation { get; set; }

        // Current leader of the organization
        public string CurrentCommander { get; set; }

        // List of all terrorists associated with the organization
        public List<Terrorist> Terrorists { get; set; }

        // Initializes a new instance of Hamas with formation date and commander
        public Hamas(DateTime formationDate, string commander)
        {
            DateOfFormation = formationDate;
            CurrentCommander = commander;
            Terrorists = new List<Terrorist>();
        }

        // Adds a new terrorist to the organization
        public void AddTerrorist(Terrorist terrorist)
        {
            Terrorists.Add(terrorist);
        }

        // Returns a list of all terrorists that are still alive
        public List<Terrorist> GetAliveTerrorists()
        {
            return Terrorists.Where(t => t.IsAlive).ToList();
        }
    }
}