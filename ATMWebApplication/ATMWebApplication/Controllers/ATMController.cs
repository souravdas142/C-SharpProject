using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ATMWebApplication.Services.Interfaces;
using ATMWebApplication.ViewModels;
using ATMWebApplication.Domain.Enums;
using ATMWebApplication.State.Interfaces;
using ATMWebApplication.Domain.Snapshots;
using ATMWebApplication.Domain.Results;

namespace ATMWebApplication.Controllers
{
    public class ATMController : Controller
    {
        private readonly IATMService _atmService;
        private readonly IStateStore _stateStore;

        public ATMController(IATMService atmService, IStateStore stateStore)
        {
            _atmService = atmService ?? throw new ArgumentNullException(nameof(atmService));
            _stateStore = stateStore ?? throw new ArgumentNullException(nameof(stateStore));
        }

        private List<DispensedNoteViewModel> GetAvailableNotes()
        {
            InventorySnapshot snapshot = _stateStore.GetInventorySnapshot();

            return snapshot.GetAll()
                .Select(x => new DispensedNoteViewModel
                {
                    Denomination = x.Key.Value,
                    Count = x.Value
                })
                .ToList();
        }

        [HttpGet]
        public IActionResult Withdraw()
        {
            InventorySnapshot inventorySnapshot = _stateStore.GetInventorySnapshot();
            WithdrawRequestViewModel model = new WithdrawRequestViewModel
            {
                
                AvailableNotes = GetAvailableNotes()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Withdraw(WithdrawRequestViewModel request)
        {
            if (!ModelState.IsValid)
            {
                request.AvailableNotes = GetAvailableNotes();
                return View(request);
            }

            WithdrawalResult result = _atmService.Withdraw(request.AccountId, request.Amount);

            AccountSnapshot accountSnapshot = _stateStore.GetAccountSnapshot(request.AccountId);

            WithdrawResponseViewModel response = MapToViewModel(result,accountSnapshot.Balance);

            return View("WithdrawResult", response);
        }

        private WithdrawResponseViewModel MapToViewModel(WithdrawalResult result, decimal balance)
        {
            WithdrawResponseViewModel response = new WithdrawResponseViewModel
            {
                IsSuccess = result.Status == WithdrawalStatus.Success,
                Message = GetMessage(result),
                Balance = balance,
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