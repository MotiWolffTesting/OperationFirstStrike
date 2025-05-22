using OperationFirstStrike;
using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Managers;
using OperationFirstStrike.Presentation;
using OperationFirstStrike.Services;

// Set up terrorist data
var terroristManager = new TerroristManager();
var yahya = new Terrorist
{
    Name = "Yahya Sinwar",
    Rank = 5,
    Weapons = new List<string> { "ak47", "knife" }
};
var mohammed = new Terrorist
{
    Name = "Mohammed Deif",
    Rank = 4,
    Weapons = new List<string> { "gun", "knife" }
};
terroristManager.Add(yahya);
terroristManager.Add(mohammed);

// Set up intel
var intelManager = new IntelligenceManager();
var intelGenerator = new IntelligenceGenerator();
intelManager.Add(intelGenerator.Generate(yahya));
intelManager.Add(intelGenerator.Generate(mohammed));

// Set up strike units
var strikeUnitManager = new StrikeUnitManager();
strikeUnitManager.RegisterUnit(new F16FighterJet());
strikeUnitManager.RegisterUnit(new HermesDrone());
strikeUnitManager.RegisterUnit(new M109Artillery());

// Set up display components
var historyWriter = new StrikeHistoryWriter();
var terroristDisplay = new TerroristDisplay(terroristManager);
var intelDisplay = new IntelligenceDisplay();
var strikeUnitDisplay = new StrikeUnitDisplay();
var strikeHistoryDisplay = new StrikeHistoryDisplay();

// Set up coordinators and controllers
var coordinator = new StrikeCoordinator(strikeUnitManager, historyWriter);
var menu = new MenuController(
    terroristManager,
    intelManager,
    coordinator,
    historyWriter,
    intelDisplay,
    terroristDisplay,
    strikeUnitDisplay,
    strikeHistoryDisplay
);

// Run simulation
var simulation = new SimulationManager(menu, terroristManager, intelManager, coordinator);
simulation.Initialize();
simulation.Run();
