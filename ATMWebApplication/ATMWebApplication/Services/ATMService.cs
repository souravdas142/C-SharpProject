using System;
using ATMWebApplication.Domain.Enums;
using ATMWebApplication.Domain.Results;
using ATMWebApplication.Services.Interfaces;
using ATMWebApplication.State.Interfaces;
using ATMWebApplication.Strategies.Interfaces;

namespace ATMWebApplication.Services
{
    /// <summary>
    /// Implementation of ATM service.
    /// 
    /// Responsible for orchestrating the withdrawal workflow.
    /// </summary>
    public sealed class ATMService : IATMService
    {
        private readonly IATMStateStore _stateStore;
        private readonly IDenominationStrategy _strategy;

        public ATMService(
            IATMStateStore stateStore,
            IDenominationStrategy strategy)
        {
            _stateStore = stateStore ?? throw new ArgumentNullException(nameof(stateStore));
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public WithdrawalResult Withdraw(string accountId, decimal amount)
        {
            // 🔹 Step 1: Basic validation
            if (string.IsNullOrWhiteSpace(accountId) || amount <= 0)
            {
                return WithdrawalResult.Failure(FailureCode.InvalidRequest);
            }

            try
            {
                // 🔹 Step 2: Get snapshots
                var accountSnapshot = _stateStore.GetAccountSnapshot(accountId);
                var inventorySnapshot = _stateStore.GetInventorySnapshot();

                // 🔹 Step 3: Fail fast - insufficient balance
                if (accountSnapshot.Balance < amount)
                {
                    return WithdrawalResult.Failure(FailureCode.InsufficientBalance);
                }

                // 🔹 Step 4: Fail fast - insufficient ATM cash
                if (inventorySnapshot.GetTotalAmount() < amount)
                {
                    return WithdrawalResult.Failure(FailureCode.InsufficientATMCash);
                }

                // 🔹 Step 5: Compute denominations
                var denominationResult = _strategy.Calculate(amount, inventorySnapshot);

                if (!denominationResult.IsSuccess)
                {
                    return WithdrawalResult.Failure(FailureCode.CannotDispenseExact);
                }

                // 🔹 Step 6: Attempt atomic commit
                return _stateStore.TryWithdraw(
                    accountId,
                    amount,
                    new System.Collections.Generic.List<Domain.ValueObjects.DispensedNote>(denominationResult.Notes)
                );
            }
            catch
            {
                // Any unexpected issue
                return WithdrawalResult.Failure(FailureCode.SystemFailure);
            }
        }
    }
}