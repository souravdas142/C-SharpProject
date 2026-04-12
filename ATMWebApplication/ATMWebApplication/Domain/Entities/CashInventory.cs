using System;
using System.Collections.Generic;
using System.Linq;
using ATMWebApplication.Domain.ValueObjects;

namespace ATMWebApplication.Domain.Entities
{
    
    // Represents the cash inventory of the ATM.
    // 
    // This is a domain entity with controlled mutability.
    // Only StateStore should modify the inventory.
    
    public sealed class CashInventory
    {
        private readonly Dictionary<Denomination, int> _notes;

        public CashInventory(Dictionary<Denomination, int> notes)
        {
            if (notes == null)
                throw new ArgumentNullException(nameof(notes));

            
            _notes = new Dictionary<Denomination, int>();

            foreach (var kvp in notes)
            {
                if (kvp.Key == null)
                    throw new ArgumentException("Denomination cannot be null.");

                if (kvp.Value < 0)
                    throw new ArgumentException("Count cannot be negative.");

                _notes[kvp.Key] = kvp.Value;
            }
        }

        
        // Returns total amount available in ATM.
        
        public decimal GetTotalAmount()
        {
            return _notes.Sum(x => x.Key.Value * x.Value);
        }

       
        // Returns a copy of current notes.
       
        public Dictionary<Denomination, int> GetSnapshot()
        {
            return new Dictionary<Denomination, int>(_notes);
        }

       
        // Checks if required notes are available.
        
        public bool HasSufficientNotes(List<DispensedNote> requiredNotes)
        {
            foreach (DispensedNote note in requiredNotes)
            {
                if (!_notes.TryGetValue(note.Denomination, out int available))
                    return false;

                if (available < note.Count)
                    return false;
            }

            return true;
        }

       
        // Deducts notes from inventory.
        // Only StateStore should call this.
     
        public void Deduct(List<DispensedNote> notesToDeduct)
        {
            if (notesToDeduct == null)
                throw new ArgumentNullException(nameof(notesToDeduct));

            foreach (DispensedNote note in notesToDeduct)
            {
                if (!_notes.ContainsKey(note.Denomination))
                    throw new InvalidOperationException("Denomination not found.");

                if (_notes[note.Denomination] < note.Count)
                    throw new InvalidOperationException("Insufficient notes.");

                _notes[note.Denomination] -= note.Count;


                if (_notes[note.Denomination] == 0)
                {
                    _notes.Remove(note.Denomination);
                }
            }
        }
    }
}