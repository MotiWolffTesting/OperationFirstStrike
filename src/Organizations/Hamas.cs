using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike
{
    public class Hamas
    {
        public DateTime DateOfFormation { get; set; }
        public string CurrentCommander { get; set; }
        public List<Terrorist> Terrorists { get; set; }

        public Hamas(DateTime formationDate, string commander)
        {
            DateOfFormation = formationDate;
            CurrentCommander = commander;
        }

        public void AddTerrorist(Terrorist terrorist)
        {
            Terrorists.Add(terrorist);
        }

        public List<Terrorist> GetAliveTerrorists()
        {
            return Terrorists.Where(t => t.IsAlive).ToList();
        }
    }
}