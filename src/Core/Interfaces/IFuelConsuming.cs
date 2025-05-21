namespace OperationFirstStrike.Core.Interfaces
{
    public interface IFuelConsuming
    {
        int FuelConsumptionRate { get; }
        void ConsumeFuel();
    }
}