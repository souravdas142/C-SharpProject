namespace ATMWebApplication.Domain.Enums
{
   
    // Represents all possible failure outcomes of a withdrawal operation.
    // 
    // This enum is used across Service, StateStore, and Controller layers
    // to ensure consistent and strongly-typed failure handling.

    public enum FailureCode
    {
       
        // The request is invalid (e.g., null, non-positive amount, invalid format).
        
        InvalidRequest = 0,

       
        // The account does not have sufficient balance.
       
        InsufficientBalance = 1,

        
        // The ATM is not operational or unavailable.
        
        ATMUnavailable = 2,

        
        // The ATM does not have enough total cash to fulfill the request.
        
        InsufficientATMCash = 3,

        
        // The requested amount cannot be dispensed using available denominations.
        
        CannotDispenseExact = 4,

       
        // The state changed during processing (e.g., concurrent modification).
        
        ConcurrentFailure = 5,

        
        // An unexpected system error occurred.
        
        SystemFailure = 6
    }
}