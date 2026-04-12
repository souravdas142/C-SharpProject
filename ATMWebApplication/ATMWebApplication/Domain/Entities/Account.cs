using System;

namespace ATMWebApplication.Domain.Entities
{
    
    // Represents a bank account.
    // 
    // This is a domain entity with controlled mutability.
    // Only StateStore should modify the balance.
    
    public sealed class Account
    {
        public string AccountId { get; }

        public decimal Balance { get; private set; }

        public Account(string accountId, decimal balance)
        {
            if (string.IsNullOrWhiteSpace(accountId))
                throw new ArgumentException("AccountId cannot be null or empty.", nameof(accountId));

            if (balance < 0)
                throw new ArgumentException("Balance cannot be negative.", nameof(balance));

            AccountId = accountId;
            Balance = balance;
        }

        
        // Deducts amount from balance.
        // Only StateStore should call this method.
        
        public void Deduct(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

            if (amount > Balance)
                throw new InvalidOperationException("Insufficient balance.");

            Balance -= amount;
        }
    }
}