using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.Core.Interfaces
{
    public interface IStrikeUnit
    {
        string Name { get; set; }
        int Ammo { get; set; }
        int Fuel { get; set; }
        int MaxFuel { get; }
        int FuelThreshold { get; }


        DateTime LastStrikeTime { get; set; }
        TimeSpan CooldownDuration { get; }
        bool IsOnCooldown => DateTime.Now.Subtract(LastStrikeTime) < CooldownDuration;


        bool NeedsRefueling => Fuel < FuelThreshold;
        void Refuel();

        bool CanStrike(string targetType);
        StrikeResult PerformStrike(Terrorist target, IntelligenceMessage intel);
    }


    public class StrikeResult
    {
        public bool Success { get; set; }
        public bool TargetEliminated { get; set; }
        public bool CollateralDamage { get; set; }
        public string? PostStrikeIntel { get; set; }
        public int FuelConsumed { get; set; }
        public int AmmoUsed { get; set; }
    }
}