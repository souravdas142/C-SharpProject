using System.Collections.Generic;
using ATMWebApplication.Domain.Results;
using ATMWebApplication.Domain.Snapshots;
using ATMWebApplication.Domain.ValueObjects;

namespace ATMWebApplication.State.Interfaces
{
     
    // Defines the contract for ATM state management.
    // 
    // Responsible for:
    // - Providing snapshots
    // - Performing atomic updates
    // - Ensuring concurrency safety
     
    public interface IStateStore
    {
         
        // Returns a snapshot of the account.
         
        AccountSnapshot GetAccountSnapshot(string accountId);

         
        // Returns a snapshot of the ATM inventory.
         
        InventorySnapshot GetInventorySnapshot();

         
        // Attempts to perform withdrawal atomically.
        // 
        // This method:
        // - Locks state
        // - Revalidates conditions
        // - Updates account and inventory
         
        WithdrawalResult TryWithdraw(
            string accountId,
            decimal amount,
            List<DispensedNote> notesToDeduct);
    }
}