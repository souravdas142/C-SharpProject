namespace ATMWebApplication.Domain.Enums
{
   
    // Represents the overall outcome status of a withdrawal operation.
    // 
    // This is intentionally kept minimal because the system guarantees:
    // - No partial success
    // - No intermediate states
    // - Atomic execution

    public enum WithdrawalStatus
    {
       
        // The withdrawal was completed successfully.
        
        Success = 0,

       
        // The withdrawal failed.
        
        Failure = 1
    }
}