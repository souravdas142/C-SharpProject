using System;
using System.Collections.Generic;
using System.Linq;
using ATMWebApplication.Domain.ValueObjects;

namespace ATMWebApplication.Domain.Entities
{
    /// <summary>
    /// Represents the cash inventory of the ATM.
    /// 
    /// This is a domain entity with controlled mutability.
    /// Only StateStore should modify the inventory.
    /// </summary>
    public sealed class CashInventory
    {
        private readonly Dictionary<Denomination, int> _notes;

        public CashInventory(Dictionary<Denomination, int> notes)
        {
            if (notes == null)
                throw new ArgumentNullException(nameof(notes));

            // Defensive copy
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

        /// <summary>
        /// Returns total amount available in ATM.
        /// </summary>
        public decimal GetTotalAmount()
        {
            return _notes.Sum(x => x.Key.Value * x.Value);
        }

        /// <summary>
        /// Returns a copy of current notes.
        /// </summary>
        public Dictionary<Denomination, int> GetSnapshot()
        {
            return new Dictionary<Denomination, int>(_notes);
        }

        /// <summary>
        /// Checks if required notes are available.
        /// </summary>
        public bool HasSufficientNotes(List<DispensedNote> requiredNotes)
        {
            foreach (var note in requiredNotes)
            {
                if (!_notes.TryGetValue(note.Denomination, out int available))
                    return false;

                if (available < note.Count)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Deducts notes from inventory.
        /// Only StateStore should call this.
        /// </summary>
        public void Deduct(List<DispensedNote> notesToDeduct)
        {
            if (notesToDeduct == null)
                throw new ArgumentNullException(nameof(notesToDeduct));

            foreach (var note in notesToDeduct)
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