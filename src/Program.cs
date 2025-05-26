using OperationFirstStrike;
using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Managers;
using OperationFirstStrike.Services;
using OperationFirstStrike.StrikeUnits;
using static OperationFirstStrike.Core.Models.IntelligenceMessage;

// Main program class that orchestrates the Operation First Strike simulation
class Program
{
    // Entry point of the application
    // Initializes and runs the enhanced military operation simulation
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("🎯 Initializing IDF Operation First Strike - Enhanced Edition...");

            // Initialize core managers for terrorist tracking, intelligence, and strike operations
            var terroristManager = new TerroristManager();
            var intelManager = new IntelligenceManager();
            var strikeUnitManager = new StrikeUnitManager();

            // Create enhanced terrorist database with realistic data
            InitializeTerroristDatabase(terroristManager);

            // Generate comprehensive intelligence reports
            GenerateIntelligenceReports(terroristManager, intelManager);

            // Initialize enhanced strike units (including new ground forces)
            InitializeStrikeUnits(strikeUnitManager);

            // Create and run the enhanced simulation
            var enhancedSimulation = new EnhancedSimulationManager(
                terroristManager,
                intelManager,
                strikeUnitManager
            );

            enhancedSimulation.Initialize();
            enhancedSimulation.Run();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"🚨 CRITICAL ERROR: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            Console.ResetColor();
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }

    // Initializes the terrorist database with realistic data
    // Creates a list of high-value targets with varying ranks and weapons
    private static void InitializeTerroristDatabase(TerroristManager terroristManager)
    {
        Console.WriteLine("📝 Creating terrorist database...");

        var terrorists = new List<Terrorist>
        {
            new Terrorist
            {
                Name = "Yahya Sinwar",
                Rank = 5,
                Weapons = new List<string> { "ak47", "knife", "gun" }
            },
            new Terrorist
            {
                Name = "Mohammed Deif",
                Rank = 5,
                Weapons = new List<string> { "m16", "knife" }
            },
            new Terrorist
            {
                Name = "Ismail Haniyeh",
                Rank = 4,
                Weapons = new List<string> { "ak47", "gun" }
            },
            new Terrorist
            {
                Name = "Khaled Meshaal",
                Rank = 4,
                Weapons = new List<string> { "gun", "knife" }
            },
            new Terrorist
            {
                Name = "Ahmed Jabari",
                Rank = 3,
                Weapons = new List<string> { "ak47" }
            },
            new Terrorist
            {
                Name = "Marwan Issa",
                Rank = 3,
                Weapons = new List<string> { "m16", "gun" }
            },
            new Terrorist
            {
                Name = "Mohammed Sinwar",
                Rank = 2,
                Weapons = new List<string> { "gun", "knife" }
            },
            new Terrorist
            {
                Name = "Rawhi Mushtaha",
                Rank = 2,
                Weapons = new List<string> { "knife" }
            },
            new Terrorist
            {
                Name = "Saleh al-Arouri",
                Rank = 4,
                Weapons = new List<string> { "ak47", "gun" }
            },
            new Terrorist
            {
                Name = "Mahmoud al-Zahar",
                Rank = 3,
                Weapons = new List<string> { "gun" }
            }
        };

        foreach (var terrorist in terrorists)
        {
            terroristManager.Add(terrorist);
        }

        Console.WriteLine($"✅ {terrorists.Count} terrorists added to database");
    }

    // Generates intelligence reports for the terrorist database
    // Creates varying numbers of reports based on terrorist rank and location
    private static void GenerateIntelligenceReports(TerroristManager terroristManager, IntelligenceManager intelManager)
    {
        Console.WriteLine("📡 Generating intelligence reports...");

        var random = new Random();
        var terrorists = terroristManager.GetAll();
        var locations = new[] { "home", "in a car", "outside", "hideout", "market", "mosque" };
        var sources = Enum.GetValues<IntelSource>();

        // Generate 20 intelligence reports with varying confidence and sources
        for (int i = 0; i < 20; i++)
        {
            var terrorist = terrorists[random.Next(terrorists.Count)];
            var location = locations[random.Next(locations.Length)];
            var source = sources[random.Next(sources.Length)];

            // Higher ranking terrorists get more reports
            var reportProbability = terrorist.Rank * 0.3;
            if (random.NextDouble() > reportProbability) continue;

            var intel = new IntelligenceMessage
            {
                Target = terrorist,
                Location = location,
                Source = source,
                ConfidenceScore = random.Next(40, 95), // Varying confidence levels
                Timestamp = DateTime.Now.AddHours(-random.Next(0, 48)) // Reports from last 48 hours
            };

            intelManager.Add(intel);
        }

        // Ensure each terrorist has at least one report
        foreach (var terrorist in terrorists)
        {
            var existingReports = intelManager.GetAllIntel().Where(i => i.Target == terrorist);
            if (!existingReports.Any())
            {
                var intel = new IntelligenceMessage
                {
                    Target = terrorist,
                    Location = locations[random.Next(locations.Length)],
                    Source = sources[random.Next(sources.Length)],
                    ConfidenceScore = random.Next(60, 85),
                    Timestamp = DateTime.Now.AddHours(-random.Next(1, 24))
                };

                intelManager.Add(intel);
            }
        }

        var totalReports = intelManager.GetAllIntel().Count;
        var validReports = intelManager.GetAllIntel().Count(i => !i.IsExpired);
        var reliableReports = intelManager.GetAllIntel().Count(i => i.IsReliable);

        Console.WriteLine($"✅ {totalReports} intelligence reports generated");
        Console.WriteLine($"   📊 {validReports} valid (not expired)");
        Console.WriteLine($"   🎯 {reliableReports} reliable (high confidence)");
    }

    // Initializes the strike units with various capabilities
    // Deploys air force assets, drones, artillery, and ground forces
    private static void InitializeStrikeUnits(StrikeUnitManager strikeUnitManager)
    {
        Console.WriteLine("⚔️ Deploying strike units...");

        // Air Force Assets
        strikeUnitManager.RegisterUnit(new F16FighterJet("F-16I Sufa", 8, 100));
        strikeUnitManager.RegisterUnit(new F16FighterJet("F-16C Block 40", 6, 90));

        // Drone Fleet
        strikeUnitManager.RegisterUnit(new HermesDrone("Hermes 450", 3, 80));
        strikeUnitManager.RegisterUnit(new HermesDrone("Hermes 900", 4, 100));

        // Artillery Units
        strikeUnitManager.RegisterUnit(new M109Artillery("M109A6 Paladin", 40, 50));

        // Ground Forces (New Feature)
        strikeUnitManager.RegisterUnit(new GroundUnit("Sayeret Matkal", 3, 50));
        strikeUnitManager.RegisterUnit(new GroundUnit("Shayetet 13", 2, 40));

        var totalUnits = strikeUnitManager.GetAllUnits().Count;
        Console.WriteLine($"✅ {totalUnits} strike units deployed and ready");

        // Display unit capabilities
        foreach (var unit in strikeUnitManager.GetAllUnits())
        {
            var capabilities = GetUnitCapabilities(unit);
            Console.WriteLine($"   🛡️ {unit.Name}: {capabilities}");
        }
    }

    // Gets a formatted string describing a unit's capabilities
    // Includes target types, ammo count, and fuel level
    private static string GetUnitCapabilities(IStrikeUnit unit)
    {
        var capabilities = new List<string>();

        if (unit.CanStrike("Building")) capabilities.Add("Buildings");
        if (unit.CanStrike("Vehicle")) capabilities.Add("Vehicles");
        if (unit.CanStrike("OpenArea")) capabilities.Add("Open Areas");

        var capabilityText = capabilities.Any() ? string.Join(", ", capabilities) : "Unknown";
        return $"{capabilityText} | {unit.Ammo} ammo | {unit.Fuel} fuel";
    }
}