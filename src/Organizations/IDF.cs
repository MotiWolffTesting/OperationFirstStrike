using IDFOperationFirstStrike.Core.Interfaces;

namespace OperationFirstStrike.Organizations
{
    public class IDF
    {
        public DateTime DateOfEstablishment { get; set; }
        public string CurrentCommander { get; set; }
        public List<IStrikeUnit> StrikeUnits { get; set; } = new();

        public IDF(DateTime established, string commander)
        {
            DateOfEstablishment = established;
            CurrentCommander = commander;
        }

        public void AddUnit(IStrikeUnit unit)
        {
            StrikeUnits.Add(unit);
        }
    }
}