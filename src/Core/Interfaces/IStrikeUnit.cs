using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.Core.Interfaces
{
    public interface IStrikeUnit
    {
        string Name { get; set; }
        int Ammo { get; set; }
        int Fuel { get; set; }

        // Enhanced properties for fuel management
        int MaxFuel { get; }
        int FuelThreshold { get; }

        // Cooldown system
        DateTime LastStrikeTime { get; set; }
        TimeSpan CooldownDuration { get; }
        bool IsOnCooldown => DateTime.Now.Subtract(LastStrikeTime) < CooldownDuration;

        // Refueling
        bool NeedsRefueling => Fuel < FuelThreshold;
        void Refuel();

        bool CanStrike(string targetType);

        // BACKWARDS COMPATIBILITY - Keep old method
        void PerformStrike(Terrorist target, IntelligenceMessage intel);

        // NEW METHOD - Enhanced strike with detailed results
        StrikeResult PerformEnhancedStrike(Terrorist target, IntelligenceMessage intel);
    }
}
