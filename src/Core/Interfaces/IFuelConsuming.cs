
// Defines capabilities for any entity that consumes fuel during operation.
namespace OperationFirstStrike.Core.Interfaces
{
    // Interface for entities that consume fuel during operation.
    public interface IFuelConsuming
    {
        // Gets the rate at which the entity consumes fuel per operational cycle.
        // Higher values indicate faster fuel consumption.
        int FuelConsumptionRate { get; }

        // Performs the fuel consumption operation based on the entity's FuelConsumptionRate.
        // This method should be called whenever the entity is active and using fuel.
        // Implementations should handle fuel depletion scenarios appropriately.
        void ConsumeFuel();
    }
}