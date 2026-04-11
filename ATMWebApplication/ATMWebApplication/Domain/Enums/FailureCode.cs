namespace ATMWebApplication.Domain.Enums
{
    /// <summary>
    /// Represents all possible failure outcomes of a withdrawal operation.
    /// 
    /// This enum is used across Service, StateStore, and Controller layers
    /// to ensure consistent and strongly-typed failure handling.
    /// </summary>
    public enum FailureCode
    {
        /// <summary>
        /// The request is invalid (e.g., null, non-positive amount, invalid format).
        /// </summary>
        InvalidRequest = 0,

        /// <summary>
        /// The account does not have sufficient balance.
        /// </summary>
        InsufficientBalance = 1,

        /// <summary>
        /// The ATM is not operational or unavailable.
        /// </summary>
        ATMUnavailable = 2,

        /// <summary>
        /// The ATM does not have enough total cash to fulfill the request.
        /// </summary>
        InsufficientATMCash = 3,

        /// <summary>
        /// The requested amount cannot be dispensed using available denominations.
        /// </summary>
        CannotDispenseExact = 4,

        /// <summary>
        /// The state changed during processing (e.g., concurrent modification).
        /// </summary>
        ConcurrentFailure = 5,

        /// <summary>
        /// An unexpected system error occurred.
        /// </summary>
        SystemFailure = 6
    }
}