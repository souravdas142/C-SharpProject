using System;
using System.Collections.Generic;
using ATMWebApplication.Domain.Enums;
using ATMWebApplication.Domain.ValueObjects;

namespace ATMWebApplication.Domain.Results
{
   
    // Represents the final result of a withdrawal operation.
    // 
    // This is returned by the Service layer and consumed by the Controller.
    
    public sealed class WithdrawalResult
    {
        public WithdrawalStatus Status { get; }

        public FailureCode? FailureCode { get; }

        public IReadOnlyList<DispensedNote> Notes { get; }

        private WithdrawalResult(
            WithdrawalStatus status,
            FailureCode? failureCode,
            List<DispensedNote> notes)
        {
            Status = status;
            FailureCode = failureCode;
            Notes = notes;
        }

        
        // Creates a successful withdrawal result.
        
        public static WithdrawalResult Success(List<DispensedNote> notes)
        {
            if (notes == null || notes.Count == 0)
                throw new ArgumentException("Notes cannot be null or empty.", nameof(notes));

            return new WithdrawalResult(
                WithdrawalStatus.Success,
                null,
                notes);
        }

        
        // Creates a failed withdrawal result.
        
        public static WithdrawalResult Failure(FailureCode failureCode)
        {
            return new WithdrawalResult(
                WithdrawalStatus.Failure,
                failureCode,
                new List<DispensedNote>());
        }
    }
}