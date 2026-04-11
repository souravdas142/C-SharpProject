namespace ATMWebApplication.Domain.Enums
{
    /// <summary>
    /// Represents the overall outcome status of a withdrawal operation.
    /// 
    /// This is intentionally kept minimal because the system guarantees:
    /// - No partial success
    /// - No intermediate states
    /// - Atomic execution
    /// </summary>
    public enum WithdrawalStatus
    {
        /// <summary>
        /// The withdrawal was completed successfully.
        /// </summary>
        Success = 0,

        /// <summary>
        /// The withdrawal failed.
        /// </summary>
        Failure = 1
    }
}