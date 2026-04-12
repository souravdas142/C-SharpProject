using ATMWebApplication.Domain.Results;
using ATMWebApplication.Domain.Snapshots;

namespace ATMWebApplication.Strategies.Interfaces
{
     
    // Defines the contract for denomination calculation strategy.
    // 
    // Implementations determine how an amount is broken down into available denominations.
     
    public interface IDenominationStrategy
    {
         
        // Calculates how to dispense the given amount using available inventory.
        // 
         
        // <param name="amount">Requested withdrawal amount</param>
        // <param name="inventorySnapshot">Read-only snapshot of ATM inventory</param>
        // <returns>
        // DenominationResult:
        // - Success with list of notes if possible
        // - Failure if exact amount cannot be dispensed
        // </returns>
        DenominationResult Calculate(decimal amount, InventorySnapshot inventorySnapshot);
    }
}