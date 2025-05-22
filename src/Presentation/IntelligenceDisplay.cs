using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.Presentation
{
    public class IntelligenceDisplay
    {
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