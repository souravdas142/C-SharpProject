using System.ComponentModel.DataAnnotations;

namespace ATMWebApplication.ViewModels
{
    /// <summary>
    /// Represents withdrawal request input from UI.
    /// </summary>
    public class WithdrawRequestViewModel
    {
        [Required]
        public string AccountId { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }
    }
}