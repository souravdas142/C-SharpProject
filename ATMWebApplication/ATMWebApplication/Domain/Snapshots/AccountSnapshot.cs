using System;
using ATMWebApplication.Domain.Entities;

namespace ATMWebApplication.Domain.Snapshots
{
    /// <summary>
    /// Represents a read-only snapshot of an account.
    /// 
    /// Used to ensure consistent reads during processing.
    /// </summary>
    public sealed class AccountSnapshot
    {
        public string AccountId { get; }

        public decimal Balance { get; }

        public AccountSnapshot(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            AccountId = account.AccountId;
            Balance = account.Balance;
        }
    }
}