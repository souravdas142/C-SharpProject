using System;

namespace ATMWebApplication.Domain.ValueObjects
{
    /// <summary>
    /// Represents a dispensed denomination and its count.
    /// Example: 500 x 2 notes.
    /// 
    /// This is a value object:
    /// - Immutable
    /// - No identity
    /// - Used in output/result modeling
    /// </summary>
    public sealed class DispensedNote
    {
        /// <summary>
        /// Denomination of the note.
        /// </summary>
        public Denomination Denomination { get; }

        /// <summary>
        /// Number of notes dispensed for this denomination.
        /// </summary>
        public int Count { get; }

        public DispensedNote(Denomination denomination, int count)
        {
            if (denomination == null)
                throw new ArgumentNullException(nameof(denomination));

            if (count <= 0)
                throw new ArgumentException("Count must be greater than zero.", nameof(count));

            Denomination = denomination;
            Count = count;
        }

        public override string ToString()
        {
            return $"{Denomination.Value} x {Count}";
        }
    }
}