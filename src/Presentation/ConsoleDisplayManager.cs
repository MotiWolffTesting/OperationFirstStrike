namespace OperationFirstStrike.Presentation
{
    // Manages console-based display operations with color formatting
    public class ConsoleDisplayManager
    {
        // Displays a title with cyan color formatting and separator lines
        public void ShowTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n==== {title} ====");
            Console.ResetColor();
        }

        // Pauses the console output and waits for user input
        public void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
        }

        // Displays a message with specified color formatting
        // Default color is white if not specified
        public void ShowMessage(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}