using System;
using System.Collections.Generic;
using ATMWebApplication.Domain.ValueObjects;

namespace ATMWebApplication.Domain.Results
{
    /// <summary>
    /// Represents the result of denomination calculation.
    /// 
    /// This is used by the Strategy layer to indicate:
    /// - Whether the amount can be dispensed
    /// - If yes, in what denominations
    /// </summary>
    public sealed class DenominationResult
    {
        public bool IsSuccess { get; }

        public IReadOnlyList<DispensedNote> Notes { get; }

        private DenominationResult(bool isSuccess, List<DispensedNote> notes)
        {
            IsSuccess = isSuccess;
            Notes = notes;
        }

        /// <summary>
        /// Creates a successful denomination result.
        /// </summary>
        public static DenominationResult Success(List<DispensedNote> notes)
        {
            if (notes == null || notes.Count == 0)
                throw new ArgumentException("Notes cannot be null or empty.", nameof(notes));

            return new DenominationResult(true, notes);
        }

        /// <summary>
        /// Creates a failed denomination result.
        /// </summary>
        public static DenominationResult Failure()
        {
            return new DenominationResult(false, new List<DispensedNote>());
        }
    }
}