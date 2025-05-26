using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.Presentation
{
    // Handles the display of intelligence information in the console
    public class IntelligenceDisplay
    {
        // Displays a summary of intelligence reports grouped by target
        // Shows total report count and number of reports per target
        public void ShowIntelligenceSummary(List<IntelligenceMessage> messages)
        {
            Console.WriteLine("\n - Intelligence Summary - ");
            Console.WriteLine($"Total reports: {messages.Count}");

            var grouped = messages.GroupBy(m => m.Target);
            foreach (var group in grouped)
            {
                Console.WriteLine($"{group.Key.Name}: {group.Count()} reports.");
            }
        }
    }
}