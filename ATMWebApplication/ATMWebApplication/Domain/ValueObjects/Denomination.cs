using System;

namespace ATMWebApplication.Domain.ValueObjects
{
     
    // Represents a currency denomination (e.g., 2000, 500, 200, 100).
    // 
    // This is a value object:
    // - Immutable
    // - Equality based on Value
    // - Used as a key in collections
     
    public sealed class Denomination : IEquatable<Denomination>, IComparable<Denomination>
    {
         
        // Monetary value of the denomination.
         
        public int Value { get; }

        public Denomination(int value)
        {
            if (value <= 0)
                throw new ArgumentException("Denomination value must be greater than zero.", nameof(value));

            Value = value;
        }

         
        // Equality based on denomination value.
         
        public bool Equals(Denomination? other)
        {
            if (other is null)
                return false;

            return Value == other.Value;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Denomination);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

         
        // Comparison based on value (used for sorting).
         
        public int CompareTo(Denomination? other)
        {
            if (other is null)
                return 1;

            return Value.CompareTo(other.Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}