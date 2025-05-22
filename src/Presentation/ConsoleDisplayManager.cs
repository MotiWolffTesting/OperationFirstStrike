namespace OperationFirstStrike.Presentation
{
    public class ConsoleDisplayManager
    {
        public void ShowTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n==== {title} ====");
            Console.ResetColor();
        }

        public void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
        }

        public void ShowMessage(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}