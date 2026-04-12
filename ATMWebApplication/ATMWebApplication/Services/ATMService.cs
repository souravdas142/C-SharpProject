using System;
using ATMWebApplication.Domain.Enums;
using ATMWebApplication.Domain.Results;
using ATMWebApplication.Domain.Snapshots;
using ATMWebApplication.Services.Interfaces;
using ATMWebApplication.State.Interfaces;
using ATMWebApplication.Strategies.Interfaces;
using ATMWebApplication.Domain.ValueObjects;

namespace ATMWebApplication.Services
{
     
    // Implementation of ATM service.
    // 
    // Responsible for orchestrating the withdrawal workflow.
     
    public sealed class ATMService : IATMService
    {
        private readonly IStateStore _stateStore;
        private readonly IDenominationStrategy _strategy;

        public ATMService(
            IStateStore stateStore,
            IDenominationStrategy strategy)
        {
            _stateStore = stateStore ?? throw new ArgumentNullException(nameof(stateStore));
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public WithdrawalResult Withdraw(string accountId, decimal amount)
        {
            // validation
            if (string.IsNullOrWhiteSpace(accountId) || amount <= 0)
            {
                return WithdrawalResult.Failure(FailureCode.InvalidRequest);
            }

            try
            {
                //  Get snapshots
                AccountSnapshot accountSnapshot = _stateStore.GetAccountSnapshot(accountId);
                InventorySnapshot inventorySnapshot = _stateStore.GetInventorySnapshot();

                // Fail fast - insufficient balance
                if (accountSnapshot.Balance < amount)
                {
                    return WithdrawalResult.Failure(FailureCode.InsufficientBalance);
                }

                // Fail fast - insufficient ATM cash
                if (inventorySnapshot.GetTotalAmount() < amount)
                {
                    return WithdrawalResult.Failure(FailureCode.InsufficientATMCash);
                }

                // Compute denominations
                DenominationResult denominationResult = _strategy.Calculate(amount, inventorySnapshot);

                if (!denominationResult.IsSuccess)
                {
                    return WithdrawalResult.Failure(FailureCode.CannotDispenseExact);
                }

                //  Attempt atomic commit
                return _stateStore.TryWithdraw(
                    accountId,
                    amount,
                    new List<DispensedNote>(denominationResult.Notes)
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