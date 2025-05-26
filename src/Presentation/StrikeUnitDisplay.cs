using OperationFirstStrike.Core.Interfaces;

namespace OperationFirstStrike.Presentation
{
    // Handles the display of strike unit information in the console
    public class StrikeUnitDisplay
    {
        // Displays a list of all available strike units with their current status
        // Shows unit name, ammo count, and fuel level for each unit
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