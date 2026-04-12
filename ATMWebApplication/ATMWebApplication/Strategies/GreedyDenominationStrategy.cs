using System;
using System.Collections.Generic;
using ATMWebApplication.Domain.Results;
using ATMWebApplication.Domain.Snapshots;
using ATMWebApplication.Domain.ValueObjects;
using ATMWebApplication.Strategies.Interfaces;

namespace ATMWebApplication.Strategies
{
     
    // Greedy implementation of denomination strategy.
    // 
    // Uses largest denomination first to compute note distribution.
     
    public sealed class GreedyDenominationStrategy : IDenominationStrategy
    {
        public DenominationResult Calculate(decimal amount, InventorySnapshot inventorySnapshot)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

            if (inventorySnapshot == null)
                throw new ArgumentNullException(nameof(inventorySnapshot));

            List<DispensedNote> result = new List<DispensedNote>();
            decimal remainingAmount = amount;

            // Get denominations in descending order
            List<Denomination> denominations = inventorySnapshot.GetDenominationsDescending();

            foreach (var denomination in denominations)
            {
                if (remainingAmount == 0)
                    break;

                int availableCount = inventorySnapshot.GetCount(denomination);

                if (availableCount <= 0)
                    continue;

                int requiredCount = (int)(remainingAmount / denomination.Value);

                int countToUse = Math.Min(requiredCount, availableCount);

                if (countToUse > 0)
                {
                    result.Add(new DispensedNote(denomination, countToUse));
                    remainingAmount -= countToUse * denomination.Value;
                }
            }

            // If fully satisfied
            if (remainingAmount == 0)
            {
                return DenominationResult.Success(result);
            }

            // Cannot form exact amount
            return DenominationResult.Failure();
        }
    }
}