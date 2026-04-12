using System;
using System.Collections.Generic;
using ATMWebApplication.Domain.Entities;
using ATMWebApplication.Domain.Enums;
using ATMWebApplication.Domain.Results;
using ATMWebApplication.Domain.Snapshots;
using ATMWebApplication.Domain.ValueObjects;
using ATMWebApplication.State.Interfaces;

namespace ATMWebApplication.State
{
     
    // In-memory implementation of ATM state store.
    // 
    // Responsible for:
    // - Holding application state
    // - Ensuring thread-safe updates
    // - Performing atomic withdrawal
     
    public sealed class InMemoryStateStore : IStateStore
    {
        private readonly object _lock = new object();

        private readonly Account _account;
        private readonly CashInventory _inventory;

        public InMemoryStateStore(Account account, CashInventory inventory)
        {
            _account = account ?? throw new ArgumentNullException(nameof(account));
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        }

        public AccountSnapshot GetAccountSnapshot(string accountId)
        {
            if (_account.AccountId != accountId)
                throw new ArgumentException("Account not found.", nameof(accountId));

            return new AccountSnapshot(_account);
        }

        public InventorySnapshot GetInventorySnapshot()
        {
            Dictionary<Denomination, int> snapshot = _inventory.GetSnapshot();
            return new InventorySnapshot(snapshot);
        }

        public WithdrawalResult TryWithdraw(
            string accountId,
            decimal amount,
            List<DispensedNote> notesToDeduct)
        {
            if (_account.AccountId != accountId)
                return WithdrawalResult.Failure(FailureCode.InvalidRequest);

            if (amount <= 0)
                return WithdrawalResult.Failure(FailureCode.InvalidRequest);

            if (notesToDeduct == null || notesToDeduct.Count == 0)
                return WithdrawalResult.Failure(FailureCode.InvalidRequest);

            lock (_lock)
            {
                // Revalidate account balance
                if (_account.Balance < amount)
                {
                    return WithdrawalResult.Failure(FailureCode.InsufficientBalance);
                }

                // Revalidate inventory
                if (!_inventory.HasSufficientNotes(notesToDeduct))
                {
                    return WithdrawalResult.Failure(FailureCode.ConcurrentFailure);
                }

                //  Perform atomic update
                try
                {
                    _account.Deduct(amount);
                    _inventory.Deduct(notesToDeduct);

                    return WithdrawalResult.Success(notesToDeduct);
                }
                catch
                {
                    // Any unexpected issue treated as system failure
                    return WithdrawalResult.Failure(FailureCode.SystemFailure);
                }
            }
        }
    }
}