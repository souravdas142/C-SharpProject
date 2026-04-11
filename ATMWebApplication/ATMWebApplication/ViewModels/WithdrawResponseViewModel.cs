using System.Collections.Generic;

namespace ATMWebApplication.ViewModels
{
    /// <summary>
    /// Represents withdrawal response for UI.
    /// </summary>
    public class WithdrawResponseViewModel
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public List<DispensedNoteViewModel> Notes { get; set; }
    }

    public class DispensedNoteViewModel
    {
        public int Denomination { get; set; }
        public int Count { get; set; }
    }
}