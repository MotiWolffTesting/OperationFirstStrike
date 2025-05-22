using OperationFirstStrike.Core.Interfaces;

namespace OperationFirstStrike.Presentation
{
    public class StrikeUnitDisplay
    {
        public void ShowStrikeUnit(List<IStrikeUnit> units)
        {
            Console.WriteLine("\n- Availavle Strike Units -");
            foreach (var unit in units)
            {
                Console.WriteLine($"{unit.Name} Summary: Ammo - {unit.Ammo}, Fuel - {unit.Fuel}.");
            }
        }
    }
}