using OperationFirstStrike.Core.Models;
using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Services;
using OperationFirstStrike.Presentation;
using OperationFirstStrike.Managers;
using static OperationFirstStrike.Core.Models.IntelligenceMessage;

namespace OperationFirstStrike.Managers
{
    public class EnhancedSimulationManager
    {
        private readonly TerroristManager _terroristManager;
        private readonly IntelligenceManager _intelManager;
        private readonly StrikeUnitManager _strikeUnitManager;
        private readonly ConsoleDisplayManager _console;
        
        // Enhanced services
        private readonly RiskAssessmentService _riskService;
        private readonly StrikeRecommendationService _recommendationService;
        private readonly SimulationTimeService _timeService;
        private readonly TerroristMovementService _movementService;
        private readonly AuthenticationService _authService;
        private readonly AnalyticsService _analyticsService;
        
        private Officer? _currentOfficer;
        private readonly List<StrikeReport> _strikeHistory = new();

        public EnhancedSimulationManager(
            TerroristManager terroristManager,
            IntelligenceManager intelManager,
            StrikeUnitManager strikeUnitManager)
        {
            _terroristManager = terroristManager;
            _intelManager = intelManager;
            _strikeUnitManager = strikeUnitManager;
            _console = new ConsoleDisplayManager();
            
            _riskService = new RiskAssessmentService();
            _recommendationService = new StrikeRecommendationService();
            _timeService = new SimulationTimeService();
            _movementService = new TerroristMovementService();
            _authService = new AuthenticationService();
            _analyticsService = new AnalyticsService();
            
            // Wire up events
            _timeService.OnTimeAdvanced += OnTimeAdvanced;
            _movementService.OnTerroristMoved += OnTerroristMoved;
        }

        public void Initialize()
        {
            // Authenticate user first
            while (_currentOfficer == null)
            {
                _currentOfficer = _authService.AuthenticateUser();
                if (_currentOfficer == null)
                {
                    Console.WriteLine("\nPress Enter to try again or type 'exit' to quit...");
                    var input = Console.ReadLine();
                    if (input?.ToLower() == "exit") return;
                }
            }

            _console.ShowTitle($"IDF OPERATION – FIRST STRIKE - {_currentOfficer.Rank} {_currentOfficer.Name}");
            _console.ShowMessage("Initializing enhanced simulation environment...", ConsoleColor.Yellow);
            Thread.Sleep(1000);
        }

        public void Run()
        {
            if (_currentOfficer == null) return;

            bool running = true;
            while (running)
            {
                ShowEnhancedMenu();
                Console.WriteLine($"\n[Current Time: {_timeService.CurrentTime:HH:mm}] Choose an option: ");
                var input = Console.ReadLine() ?? "";

                try
                {
                    switch (input)
                    {
                        case "1": ShowTerroristInformation(); break;
                        case "2": ShowIntelligenceAnalysis(); break;
                        case "3": ShowStrikeUnitsStatus(); break;
                        case "4": ShowStrikeHistory(); break;
                        case "5": ShowTargetPrioritization(); break;
                        case "6": ConductAdvancedStrike(); break;
                        case "7": ShowAnalytics(); break;
                        case "8": SearchTerrorists(); break;
                        case "9": AdvanceTime(); break;
                        case "10": RefuelUnits(); break;
                        case "11": ShowMissionStatus(); break;
                        case "0": 
                            _console.ShowMessage($"Mission concluded. Stay safe, {_currentOfficer.Rank} {_currentOfficer.Name}.", ConsoleColor.Red);
                            running = false;
                            break;
                        default:
                            _console.ShowMessage("Invalid input. Try again.", ConsoleColor.DarkYellow);
                            break;
                    }

                    if (running && input != "0")
                    {
                        Console.WriteLine("\nPress Enter to continue...");
                        Console.ReadLine();
                    }
                }
                catch (Exception ex)
                {
                    _console.ShowMessage($"An error occurred: {ex.Message}", ConsoleColor.Red);
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                }
            }
        }

        private void ShowEnhancedMenu()
        {
            Console.Clear();
            _console.ShowTitle("🎯 IDF ENHANCED OPERATIONAL COMMAND CENTER");
            Console.WriteLine($"Officer: {_currentOfficer?.Rank} {_currentOfficer?.Name}");
            Console.WriteLine($"Simulation Time: {_timeService.CurrentTime:yyyy-MM-dd HH:mm}");
            Console.WriteLine($"Elapsed: {_timeService.ElapsedTime.TotalHours:F1} hours");
            Console.WriteLine();
            
            Console.WriteLine("📊 INTELLIGENCE & RECONNAISSANCE");
            Console.WriteLine("  1. Terrorist Database");
            Console.WriteLine("  2. Intelligence Analysis (with expiry & confidence)");
            Console.WriteLine("  8. Search Terrorists");
            Console.WriteLine();
            
            Console.WriteLine("⚔️  STRIKE OPERATIONS");
            Console.WriteLine("  3. Strike Units Status (fuel, cooldown, ammo)");
            Console.WriteLine("  4. Strike History & Logs");
            Console.WriteLine("  5. Target Prioritization (risk assessment)");
            Console.WriteLine("  6. Execute Advanced Strike (with recommendations)");
            Console.WriteLine();
            
            Console.WriteLine("📈 MISSION MANAGEMENT");
            Console.WriteLine("  7. Analytics & Statistics");
            Console.WriteLine("  9. Advance Time (triggers movement, cooldowns)");
            Console.WriteLine("  10. Refuel & Resupply Units");
            Console.WriteLine("  11. Mission Status & Victory Conditions");
            Console.WriteLine();
            
            Console.WriteLine("  0. Exit Mission");
        }

        private void ShowTerroristInformation()
        {
            _console.ShowTitle("🎯 TERRORIST DATABASE");
            var terrorists = _terroristManager.GetAll();
            
            Console.WriteLine($"Total Terrorists: {terrorists.Count}");
            Console.WriteLine($"Active Threats: {terrorists.Count(t => t.IsAlive)}");
            Console.WriteLine($"Eliminated: {terrorists.Count(t => !t.IsAlive)}");
            Console.WriteLine();

            foreach (var terrorist in terrorists)
            {
                var weaponScore = _terroristManager.GetWeaponScore(terrorist);
                var status = terrorist.IsAlive ? "🔴 ACTIVE" : "✅ NEUTRALIZED";
                
                Console.WriteLine($"{status} {terrorist.Name}");
                Console.WriteLine($"  Rank: {terrorist.Rank} | Danger Score: {weaponScore}");
                Console.WriteLine($"  Weapons: {string.Join(", ", terrorist.Weapons)}");
                
                // Show latest intel
                var latestIntel = _intelManager.GetLatestFromTarget(terrorist).FirstOrDefault();
                if (latestIntel != null)
                {
                    var expiredText = latestIntel.IsExpired ? " [EXPIRED]" : "";
                    Console.WriteLine($"  Last Intel: {latestIntel.Location} ({latestIntel.Timestamp:MM/dd HH:mm}) - {latestIntel.ConfidenceScore}% confidence{expiredText}");
                }
                Console.WriteLine();
            }
        }

        private void ShowIntelligenceAnalysis()
        {
            _console.ShowTitle("📡 INTELLIGENCE ANALYSIS");
            var allIntel = _intelManager.GetAllIntel();
            var validIntel = allIntel.Where(i => !i.IsExpired).ToList();
            var reliableIntel = allIntel.Where(i => i.IsReliable).ToList();

            Console.WriteLine($"Total Intelligence Reports: {allIntel.Count}");
            Console.WriteLine($"Valid (not expired): {validIntel.Count}");
            Console.WriteLine($"Reliable (high confidence + valid): {reliableIntel.Count}");
            Console.WriteLine();

            // Group by source
            Console.WriteLine("📊 INTEL BY SOURCE:");
            var sourceGroups = allIntel.GroupBy(i => i.Source);
            foreach (var group in sourceGroups)
            {
                var avgConfidence = group.Average(i => i.ConfidenceScore);
                Console.WriteLine($"  {group.Key}: {group.Count()} reports (avg confidence: {avgConfidence:F1}%)");
            }
            Console.WriteLine();

            // Most reported terrorist
            var reportCounts = validIntel.GroupBy(i => i.Target).ToDictionary(g => g.Key, g => g.Count());
            if (reportCounts.Any())
            {
                var mostReported = reportCounts.OrderByDescending(kvp => kvp.Value).First();
                Console.WriteLine($"🎯 MOST TRACKED TARGET: {mostReported.Key.Name} ({mostReported.Value} valid reports)");
                
                var recentReports = validIntel.Where(i => i.Target == mostReported.Key)
                    .OrderByDescending(i => i.Timestamp).Take(3);
                
                foreach (var report in recentReports)
                {
                    Console.WriteLine($"  • {report.Location} - {report.ConfidenceScore}% confidence ({report.Source}) [{report.Timestamp:MM/dd HH:mm}]");
                }
            }
        }

        private void ShowStrikeUnitsStatus()
        {
            _console.ShowTitle("⚔️ STRIKE UNITS STATUS");
            var units = _strikeUnitManager.GetAllUnits();

            foreach (var unit in units)
            {
                Console.WriteLine($"🛡️ {unit.Name}");
                Console.WriteLine($"  Ammo: {unit.Ammo} | Fuel: {unit.Fuel}/{unit.MaxFuel}");
                
                var status = new List<string>();
                if (unit.IsOnCooldown)
                {
                    var cooldownRemaining = unit.CooldownDuration - (DateTime.Now - unit.LastStrikeTime);
                    status.Add($"COOLDOWN ({cooldownRemaining.TotalMinutes:F0}m remaining)");
                }
                if (unit.NeedsRefueling) status.Add("NEEDS REFUEL");
                if (unit.Ammo == 0) status.Add("NO AMMO");
                
                var statusText = status.Any() ? string.Join(" | ", status) : "READY";
                var statusColor = status.Any() ? ConsoleColor.Yellow : ConsoleColor.Green;
                
                Console.ForegroundColor = statusColor;
                Console.WriteLine($"  Status: {statusText}");
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        private void ShowTargetPrioritization()
        {
            _console.ShowTitle("🎯 TARGET PRIORITIZATION & RISK ASSESSMENT");
            var terrorists = _terroristManager.GetAll().Where(t => t.IsAlive).ToList();
            var mostDangerous = _terroristManager.GetMostDangerousTerrorist(terrorists);
            
            if (mostDangerous == null)
            {
                Console.WriteLine("No active targets available.");
                return;
            }

            var weaponScore = _terroristManager.GetWeaponScore(mostDangerous);
            var latestIntel = _intelManager.GetLatestFromTarget(mostDangerous).FirstOrDefault();
            
            Console.WriteLine($"🔥 HIGHEST PRIORITY TARGET: {mostDangerous.Name}");
            Console.WriteLine($"Danger Score: {weaponScore} | Rank: {mostDangerous.Rank}");
            Console.WriteLine($"Weapons: {string.Join(", ", mostDangerous.Weapons)}");
            
            if (latestIntel != null)
            {
                Console.WriteLine($"Last Known Location: {latestIntel.Location}");
                Console.WriteLine($"Intel Confidence: {latestIntel.ConfidenceScore}%");
                Console.WriteLine($"Intel Age: {(DateTime.Now - latestIntel.Timestamp).TotalHours:F1} hours");
                
                // Risk assessment for each available unit
                Console.WriteLine("\n📊 RISK ASSESSMENT BY UNIT:");
                var availableUnits = _strikeUnitManager.GetAllUnits().Where(u => u.Ammo > 0).ToList();
                
                foreach (var unit in availableUnits)
                {
                    var risk = _riskService.CalculateRisk(mostDangerous, latestIntel, unit, weaponScore);
                    var riskColor = risk.Level switch
                    {
                        RiskLevel.Low => ConsoleColor.Green,
                        RiskLevel.Medium => ConsoleColor.Yellow,
                        RiskLevel.High => ConsoleColor.Red,
                        RiskLevel.Critical => ConsoleColor.DarkRed,
                        _ => ConsoleColor.White
                    };
                    
                    Console.ForegroundColor = riskColor;
                    Console.WriteLine($"  {unit.Name}: {risk.Level} ({risk.Score}/100)");
                    Console.ResetColor();
                    Console.WriteLine($"    {risk.Reasoning}");
                }
            }
        }

        private void ConductAdvancedStrike()
        {
            _console.ShowTitle("⚔️ ADVANCED STRIKE EXECUTION");
            
            // Select target
            var aliveTerrorists = _terroristManager.GetAll().Where(t => t.IsAlive).ToList();
            if (!aliveTerrorists.Any())
            {
                Console.WriteLine("✅ ALL THREATS ELIMINATED - MISSION ACCOMPLISHED!");
                return;
            }

            Console.WriteLine("Available targets:");
            for (int i = 0; i < aliveTerrorists.Count; i++)
            {
                var terrorist = aliveTerrorists[i];
                var score = _terroristManager.GetWeaponScore(terrorist);
                Console.WriteLine($"{i + 1}. {terrorist.Name} (Danger: {score})");
            }

            Console.Write("\nSelect target: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > aliveTerrorists.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            var target = aliveTerrorists[choice - 1];
            var latestIntel = _intelManager.GetLatestFromTarget(target).FirstOrDefault();
            
            if (latestIntel == null)
            {
                Console.WriteLine("No intelligence available for this target.");
                return;
            }

            if (latestIntel.IsExpired)
            {
                Console.WriteLine($"⚠️ WARNING: Intelligence is {(DateTime.Now - latestIntel.Timestamp).TotalHours:F1} hours old!");
                Console.Write("Proceed anyway? (y/n): ");
                if (Console.ReadLine()?.ToLower() != "y") return;
            }

            // Get strike recommendation
            var recommendation = _recommendationService.GetRecommendation(latestIntel, _strikeUnitManager.GetAllUnits());
            
            Console.WriteLine($"\n🎯 TARGET: {target.Name} at {latestIntel.Location}");
            Console.WriteLine($"Intel Confidence: {latestIntel.ConfidenceScore}%");
            
            if (recommendation.RecommendedUnit != null)
            {
                Console.WriteLine($"\n💡 RECOMMENDED UNIT: {recommendation.RecommendedUnit.Name}");
                Console.WriteLine($"Effectiveness: {recommendation.EffectivenessScore:F1}/10");
                Console.WriteLine($"Reasoning: {recommendation.Reasoning}");
                
                if (recommendation.Alternatives.Any())
                {
                    Console.WriteLine("\nAlternatives:");
                    foreach (var alt in recommendation.Alternatives)
                    {
                        Console.WriteLine($"  • {alt}");
                    }
                }

                // Risk assessment
                var weaponScore = _terroristManager.GetWeaponScore(target);
                var risk = _riskService.CalculateRisk(target, latestIntel, recommendation.RecommendedUnit, weaponScore);
                
                Console.ForegroundColor = risk.Level switch
                {
                    RiskLevel.Low => ConsoleColor.Green,
                    RiskLevel.Medium => ConsoleColor.Yellow,
                    RiskLevel.High => ConsoleColor.Red,
                    _ => ConsoleColor.DarkRed
                };
                Console.WriteLine($"\n⚠️ RISK LEVEL: {risk.Level} ({risk.Score}/100)");
                Console.ResetColor();
                
                foreach (var factor in risk.Factors)
                {
                    Console.WriteLine($"  • {factor}");
                }

                Console.Write("\nProceed with strike? (y/n): ");
                if (Console.ReadLine()?.ToLower() == "y")
                {
                    ExecuteStrike(target, latestIntel, recommendation.RecommendedUnit);
                }
            }
            else
            {
                Console.WriteLine($"\n❌ {recommendation.Reasoning}");
            }
        }

        private void ExecuteStrike(Terrorist target, IntelligenceMessage intel, IStrikeUnit unit)
        {
            Console.WriteLine($"\n🚀 Executing strike with {unit.Name}...");
            Thread.Sleep(2000);

            var result = unit.PerformStrike(target, intel);
            var report = new StrikeReport
            {
                Unit = unit,
                Target = target,
                Intel = intel,
                Success = result.Success,
                Timestamp = DateTime.Now
            };

            _strikeHistory.Add(report);
            _analyticsService.RecordStrike(report);

            // Display results
            if (result.Success)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✅ STRIKE SUCCESSFUL!");
                Console.ResetColor();
                
                if (result.TargetEliminated)
                {
                    Console.WriteLine($"🎯 Target {target.Name} eliminated");
                }
                else
                {
                    Console.WriteLine($"🎯 Target {target.Name} captured");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ STRIKE FAILED");
                Console.ResetColor();
            }

            Console.WriteLine($"Resources used: {result.AmmoUsed} ammo, {result.FuelConsumed} fuel");
            
            if (result.CollateralDamage)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("⚠️ Collateral damage reported");
                Console.ResetColor();
            }

            if (!string.IsNullOrEmpty(result.PostStrikeIntel))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"📡 Post-strike intel: {result.PostStrikeIntel}");
                Console.ResetColor();
            }

            // Log the action
            Console.WriteLine($"\n📝 Strike logged by {_currentOfficer?.Rank} {_currentOfficer?.Name}");
        }

        private void ShowAnalytics()
        {
            _console.ShowTitle("📈 MISSION ANALYTICS");
            
            var missionStats = _analyticsService.GetMissionStatistics();
            var weaponStats = _analyticsService.GetWeaponStatistics();

            Console.WriteLine("📊 MISSION STATISTICS:");
            Console.WriteLine($"Total Strikes: {missionStats.TotalStrikes}");
            Console.WriteLine($"Success Rate: {missionStats.SuccessRate:F1}%");
            Console.WriteLine($"Terrorists Eliminated: {missionStats.TerroristsEliminated}");
            
            if (missionStats.CollateralIncidents > 0)
            {
                Console.WriteLine($"Collateral Incidents: {missionStats.CollateralIncidents}");
            }

            Console.WriteLine("\n🔫 WEAPON EFFECTIVENESS:");
            foreach (var weapon in weaponStats)
            {
                Console.WriteLine($"{weapon.WeaponName}: {weapon.SuccessRate:F1}% success ({weapon.SuccessfulStrikes}/{weapon.TimesUsed})");
            }

            // Victory conditions check
            var totalTerrorists = _terroristManager.GetAll().Count;
            var eliminatedTerrorists = _terroristManager.GetAll().Count(t => !t.IsAlive);
            var eliminationPercentage = (float)eliminatedTerrorists / totalTerrorists * 100;

            Console.WriteLine($"\n🏆 MISSION PROGRESS:");
            Console.WriteLine($"Threats Eliminated: {eliminationPercentage:F1}% ({eliminatedTerrorists}/{totalTerrorists})");
            
            if (eliminationPercentage >= 80)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("🎉 MISSION SUCCESS CRITERIA MET!");
                Console.ResetColor();
            }
        }

        private void SearchTerrorists()
        {
            _console.ShowTitle("🔍 TERRORIST SEARCH");
            
            Console.Write("Enter search term (name or weapon): ");
            var searchTerm = Console.ReadLine();
            
            if (string.IsNullOrEmpty(searchTerm))
            {
                Console.WriteLine("Invalid search term.");
                return;
            }

            var results = _analyticsService.SearchTerrorists(_terroristManager.GetAll(), searchTerm);
            
            if (!results.Any())
            {
                Console.WriteLine($"No terrorists found matching '{searchTerm}'");
                return;
            }

            Console.WriteLine($"\n🎯 SEARCH RESULTS for '{searchTerm}':");
            foreach (var terrorist in results)
            {
                var status = terrorist.IsAlive ? "🔴 ACTIVE" : "✅ NEUTRALIZED";
                var score = _terroristManager.GetWeaponScore(terrorist);
                
                Console.WriteLine($"{status} {terrorist.Name}");
                Console.WriteLine($"  Rank: {terrorist.Rank} | Danger Score: {score}");
                Console.WriteLine($"  Weapons: {string.Join(", ", terrorist.Weapons)}");
                
                var intelCount = _intelManager.GetAllIntel().Count(i => i.Target == terrorist);
                Console.WriteLine($"  Intelligence reports: {intelCount}");
                Console.WriteLine();
            }
        }

        private void AdvanceTime()
        {
            _console.ShowTitle("⏰ TIME ADVANCEMENT");
            
            Console.WriteLine("Select time advancement:");
            Console.WriteLine("1. 30 minutes");
            Console.WriteLine("2. 1 hour");
            Console.WriteLine("3. 2 hours");
            Console.WriteLine("4. 6 hours");
            Console.WriteLine("5. Custom");
            
            Console.Write("Choose option: ");
            var choice = Console.ReadLine();
            
            TimeSpan timeToAdvance = choice switch
            {
                "1" => TimeSpan.FromMinutes(30),
                "2" => TimeSpan.FromHours(1),
                "3" => TimeSpan.FromHours(2),
                "4" => TimeSpan.FromHours(6),
                "5" => GetCustomTimeAdvancement(),
                _ => TimeSpan.Zero
            };

            if (timeToAdvance > TimeSpan.Zero)
            {
                _timeService.AdvanceTime(timeToAdvance);
                Console.WriteLine($"⏰ Time advanced by {timeToAdvance.TotalHours:F1} hours");
                Console.WriteLine($"Current simulation time: {_timeService.CurrentTime:yyyy-MM-dd HH:mm}");
            }
        }

        private TimeSpan GetCustomTimeAdvancement()
        {
            Console.Write("Enter hours to advance: ");
            if (double.TryParse(Console.ReadLine(), out double hours) && hours > 0)
            {
                return TimeSpan.FromHours(hours);
            }
            return TimeSpan.Zero;
        }

        private void RefuelUnits()
        {
            _console.ShowTitle("⛽ REFUEL & RESUPPLY");
            
            var units = _strikeUnitManager.GetAllUnits().Where(u => u.NeedsRefueling || u.Ammo < 3).ToList();
            
            if (!units.Any())
            {
                Console.WriteLine("All units are fully supplied and fueled.");
                return;
            }

            Console.WriteLine("Units requiring maintenance:");
            for (int i = 0; i < units.Count; i++)
            {
                var unit = units[i];
                Console.WriteLine($"{i + 1}. {unit.Name} - Fuel: {unit.Fuel}/{unit.MaxFuel}, Ammo: {unit.Ammo}");
            }

            Console.Write("Select unit to refuel (or 'all'): ");
            var input = Console.ReadLine();

            if (input?.ToLower() == "all")
            {
                foreach (var unit in units)
                {
                    unit.Refuel();
                    Console.WriteLine($"✅ {unit.Name} refueled and resupplied");
                }
            }
            else if (int.TryParse(input, out int choice) && choice > 0 && choice <= units.Count)
            {
                var unit = units[choice - 1];
                unit.Refuel();
                Console.WriteLine($"✅ {unit.Name} refueled and resupplied");
            }
        }

        private void ShowMissionStatus()
        {
            _console.ShowTitle("🏆 MISSION STATUS & VICTORY CONDITIONS");
            
            var totalTerrorists = _terroristManager.GetAll().Count;
            var activeTerrorists = _terroristManager.GetAll().Count(t => t.IsAlive);
            var eliminationRate = (float)(totalTerrorists - activeTerrorists) / totalTerrorists * 100;
            
            var missionStats = _analyticsService.GetMissionStatistics();
            
            Console.WriteLine($"📊 CURRENT STATUS:");
            Console.WriteLine($"Mission Duration: {_timeService.ElapsedTime.TotalHours:F1} hours");
            Console.WriteLine($"Active Threats: {activeTerrorists}/{totalTerrorists}");
            Console.WriteLine($"Elimination Rate: {eliminationRate:F1}%");
            Console.WriteLine($"Strike Success Rate: {missionStats.SuccessRate:F1}%");
            
            Console.WriteLine($"\n🎯 VICTORY CONDITIONS:");
            var conditions = new List<(string condition, bool met, string status)>
            {
                ("Eliminate 80% of terrorists", eliminationRate >= 80, $"{eliminationRate:F1}%/80%"),
                ("Maintain 70% strike success rate", missionStats.SuccessRate >= 70, $"{missionStats.SuccessRate:F1}%/70%"),
                ("Complete within 48 hours", _timeService.ElapsedTime.TotalHours <= 48, $"{_timeService.ElapsedTime.TotalHours:F1}h/48h")
            };

            foreach (var (condition, met, status) in conditions)
            {
                var icon = met ? "✅" : "❌";
                var color = met ? ConsoleColor.Green : ConsoleColor.Red;
                
                Console.ForegroundColor = color;
                Console.WriteLine($"{icon} {condition} - {status}");
                Console.ResetColor();
            }

            var missionSuccess = conditions.All(c => c.met);
            if (missionSuccess)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n🎉 MISSION ACCOMPLISHED, {_currentOfficer?.Rank} {_currentOfficer?.Name}!");
                Console.ResetColor();
            }
        }

        private void ShowStrikeHistory()
        {
            _console.ShowTitle("📋 DETAILED STRIKE HISTORY");
            
            if (!_strikeHistory.Any())
            {
                Console.WriteLine("No strikes conducted yet.");
                return;
            }

            foreach (var strike in _strikeHistory.OrderByDescending(s => s.Timestamp))
            {
                var successIcon = strike.Success ? "✅" : "❌";
                Console.WriteLine($"{successIcon} {strike.Timestamp:MM/dd HH:mm} - {strike.Unit?.Name ?? "Unknown"}");
                Console.WriteLine($"   Target: {strike.Target?.Name ?? "Unknown"}");
                Console.WriteLine($"   Location: {strike.Intel?.Location ?? "Unknown"}");
                Console.WriteLine($"   Officer: {_currentOfficer?.Rank} {_currentOfficer?.Name}");
                Console.WriteLine();
            }
        }

        private void OnTimeAdvanced(TimeSpan timeAdvanced)
        {
            // Update terrorist locations
            _movementService.UpdateTerroristLocations(_terroristManager.GetAll(), timeAdvanced);
            
            Console.WriteLine($"⏰ {timeAdvanced.TotalMinutes} minutes passed...");
        }

        private void OnTerroristMoved(Terrorist terrorist, string newLocation)
        {
            if (terrorist.IsAlive)
            {
                var newIntel = new IntelligenceMessage
                {
                    Target = terrorist,
                    Location = newLocation,
                    Timestamp = _timeService.CurrentTime,
                    ConfidenceScore = new Random().Next(60, 90),
                    Source = (IntelSource)new Random().Next(0, 6)
                };
                
                _intelManager.Add(newIntel);
                Console.WriteLine($"📡 New intel: {terrorist.Name} spotted at {newLocation}");
            }
        }
    }
}