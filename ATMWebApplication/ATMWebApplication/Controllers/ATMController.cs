using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ATMWebApplication.Services.Interfaces;
using ATMWebApplication.ViewModels;
using ATMWebApplication.Domain.Enums;
using ATMWebApplication.State.Interfaces;

namespace ATMWebApplication.Controllers
{
    public class ATMController : Controller
    {
        private readonly IATMService _atmService;
        private readonly IATMStateStore _atmStateStore;

        public ATMController(IATMService atmService, IATMStateStore atmStateStore)
        {
            _atmService = atmService ?? throw new ArgumentNullException(nameof(atmService));
            _atmStateStore = atmStateStore ?? throw new ArgumentNullException(nameof(atmStateStore));
        }

        [HttpGet]
        public IActionResult Withdraw()
        {
            var inventorySnapshot = _atmStateStore.GetInventorySnapshot();
            var model = new WithdrawRequestViewModel
            {
                //AccountId = "xyz",
                AvailableNotes = inventorySnapshot.GetAll()
                                    .Select(x => new DispensedNoteViewModel
                                    {
                                        Denomination = x.Key.Value,
                                        Count = x.Value
                                    })
                                    .ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Withdraw(WithdrawRequestViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = _atmService.Withdraw(request.AccountId, request.Amount);

            var response = MapToViewModel(result);

            return View("WithdrawResult", response);
        }

        private WithdrawResponseViewModel MapToViewModel(Domain.Results.WithdrawalResult result)
        {
            var response = new WithdrawResponseViewModel
            {
                IsSuccess = result.Status == WithdrawalStatus.Success,
                Message = GetMessage(result),
                Notes = result.Notes
                    .Select(n => new DispensedNoteViewModel
                    {
                        Denomination = n.Denomination.Value,
                        Count = n.Count
                    })
                    .ToList()
            };

            return response;
        }

        private string GetMessage(Domain.Results.WithdrawalResult result)
        {
            if (result.Status == WithdrawalStatus.Success)
                return "Withdrawal successful";

            return result.FailureCode switch
            {
                FailureCode.InvalidRequest => "Invalid request",
                FailureCode.InsufficientBalance => "Insufficient balance",
                FailureCode.InsufficientATMCash => "ATM has insufficient cash",
                FailureCode.CannotDispenseExact => "Cannot dispense exact amount",
                FailureCode.ConcurrentFailure => "Transaction conflict, please try again",
                FailureCode.SystemFailure => "System error occurred",
                _ => "Unknown error"
            };
        }
    }
}