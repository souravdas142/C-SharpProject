using ATMWebApplication.Domain.Results;

namespace ATMWebApplication.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for ATM service operations.
    /// 
    /// Responsible for orchestrating the withdrawal workflow.
    /// </summary>
    public interface IATMService
    {
        /// <summary>
        /// Performs withdrawal operation.
        /// 
        /// This method:
        /// - Validates request
        /// - Retrieves snapshots
        /// - Invokes denomination strategy
        /// - Calls state store for atomic update
        /// </summary>
        WithdrawalResult Withdraw(string accountId, decimal amount);
    }
}