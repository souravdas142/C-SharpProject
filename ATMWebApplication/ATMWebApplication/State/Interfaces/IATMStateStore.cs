using System.Collections.Generic;
using ATMWebApplication.Domain.Results;
using ATMWebApplication.Domain.Snapshots;
using ATMWebApplication.Domain.ValueObjects;

namespace ATMWebApplication.State.Interfaces
{
    /// <summary>
    /// Defines the contract for ATM state management.
    /// 
    /// Responsible for:
    /// - Providing snapshots
    /// - Performing atomic updates
    /// - Ensuring concurrency safety
    /// </summary>
    public interface IATMStateStore
    {
        /// <summary>
        /// Returns a snapshot of the account.
        /// </summary>
        AccountSnapshot GetAccountSnapshot(string accountId);

        /// <summary>
        /// Returns a snapshot of the ATM inventory.
        /// </summary>
        InventorySnapshot GetInventorySnapshot();

        /// <summary>
        /// Attempts to perform withdrawal atomically.
        /// 
        /// This method:
        /// - Locks state
        /// - Revalidates conditions
        /// - Updates account and inventory
        /// </summary>
        WithdrawalResult TryWithdraw(
            string accountId,
            decimal amount,
            List<DispensedNote> notesToDeduct);
    }
}