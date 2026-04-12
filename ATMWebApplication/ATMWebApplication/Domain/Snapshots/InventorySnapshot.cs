using System;
using System.Collections.Generic;
using System.Linq;
using ATMWebApplication.Domain.ValueObjects;

namespace ATMWebApplication.Domain.Snapshots
{
     
    // Represents a read-only snapshot of ATM inventory.
    // 
    // Used by Strategy layer to perform safe calculations.
     
    public sealed class InventorySnapshot
    {
        private readonly IReadOnlyDictionary<Denomination, int> _notes;

        public InventorySnapshot(Dictionary<Denomination, int> notes)
        {
            if (notes == null)
                throw new ArgumentNullException(nameof(notes));

            // Wrap as read-only to prevent external modification
            _notes = new Dictionary<Denomination, int>(notes);
        }

         
        // Returns all denominations sorted in descending order.
         
        public List<Denomination> GetDenominationsDescending()
        {
            return _notes.Keys
                .OrderByDescending(d => d.Value)
                .ToList();
        }

         
        // Returns available count for a denomination.
         
        public int GetCount(Denomination denomination)
        {
            if (denomination == null)
                throw new ArgumentNullException(nameof(denomination));

            return _notes.TryGetValue(denomination, out int count)
                ? count
                : 0;
        }

         
        // Returns total amount available in snapshot.
         
        public decimal GetTotalAmount()
        {
            return _notes.Sum(x => x.Key.Value * x.Value);
        }

         
        // Returns all notes as read-only dictionary.
         
        public IReadOnlyDictionary<Denomination, int> GetAll()
        {
            return _notes;
        }
    }
}