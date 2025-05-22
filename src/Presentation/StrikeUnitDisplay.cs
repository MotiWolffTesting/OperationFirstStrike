using OperationFirstStrike.Core.Interfaces;

namespace OperationFirstStrike.Presentation
{
    public class StrikeUnitDisplay
    {
        public void ShowStrikeUnits(List<IStrikeUnit> units)
        {
            Console.WriteLine("\n- Available Strike Units -");
            foreach (var unit in units)
            {
                Console.WriteLine($"{unit.Name} Summary: Ammo - {unit.Ammo}, Fuel - {unit.Fuel}.");
            }
        }
    }
}