using ATMWebApplication.Domain.Results;

namespace ATMWebApplication.Services.Interfaces
{
     
    // Defines the contract for ATM service operations.
    // 
    // Responsible for orchestrating the withdrawal workflow.
     
    public interface IATMService
    {
         
        // Performs withdrawal operation.
        // 
        // This method:
        // - Validates request
        // - Retrieves snapshots
        // - Invokes denomination strategy
        // - Calls state store for atomic update
         
        WithdrawalResult Withdraw(string accountId, decimal amount);
    }
}