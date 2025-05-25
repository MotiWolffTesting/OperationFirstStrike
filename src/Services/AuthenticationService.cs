using OperationFirstStrike.Presentation;

namespace OperationFirstStrike.Services
{
    public class Officer
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Rank { get; set; } = string.Empty;
        public DateTime LoginTime { get; set; }

    }
    public class AuthenticationService
    {
        private readonly Dictionary<string, string> _validCodes = new()
        {
            {"KODKOD", "Aluf-Mishne"},
            {"MISHNE", "Sgan-Aluf"},
            {"KODKODON", "Rav-Seren"},
            {"BARZELAN", "Seren"}
        };

        public Officer? AuthenticateUser()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("- CLASSIFIED ACCESS -");
            Console.WriteLine("- IDF OPERATION FIRST STRIKE -");
            Console.ResetColor();

            Console.WriteLine("\nEnter your name: ");
            var name = Console.ReadLine()!;

            Console.WriteLine("Enter autentication code: ");
            var code = Console.ReadLine()!.ToUpper();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(code))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Authentication failed: Invalid credentials");
                Console.ResetColor();
                return null;
            }

            if (_validCodes.ContainsKey(code))
            {
                var officer = new Officer
                {
                    Name = name,
                    Code = code,
                    Rank = _validCodes[code],
                    LoginTime = DateTime.Now
                };

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nAuthentication successful. Welcome, {officer.Rank} {officer.Name}");
                Console.ResetColor();
                Thread.Sleep(2000);

                return officer;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Authentication failed: Invalid authorization code");
            Console.ResetColor();
            return null;
        }
    }
}