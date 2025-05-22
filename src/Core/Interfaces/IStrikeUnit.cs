using OperationFirstStrike.Core.Models;


namespace OperationFirstStrike.Core.Interfaces
{
    public interface IStrikeUnit
    {
        string Name { get; set; }
        int Ammo { get; set; }
        int Fuel { get; set; }

        bool CanStrike(string targetType);
        void PerformStrike(Terrorist target, IntelligenceMessage intel);
    }
}