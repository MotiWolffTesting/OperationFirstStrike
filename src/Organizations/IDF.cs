using OperationFirstStrike.Core.Interfaces;

namespace OperationFirstStrike.Organizations
{
    // Represents the Israel Defense Forces (IDF) and its strike units
    public class IDF
    {
        // When the IDF was established
        public DateTime DateOfEstablishment { get; set; }

        // Current Chief of Staff of the IDF
        public string CurrentCommander { get; set; }

        // Collection of all available strike units in the IDF
        public List<IStrikeUnit> StrikeUnits { get; set; } = new();

        // Initializes a new instance of the IDF with establishment date and commander
        public IDF(DateTime established, string commander)
        {
            DateOfEstablishment = established;
            CurrentCommander = commander;
        }

        // Adds a new strike unit to the IDF's forces
        public void AddUnit(IStrikeUnit unit)
        {
            StrikeUnits.Add(unit);
        }
    }
}