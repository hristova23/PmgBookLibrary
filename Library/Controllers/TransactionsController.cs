using Library.Core.Contracts;
using Library.Core.Models.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Controllers
{
    public class TransactionsController : BaseController
    {
        private readonly ITransactionService transactionService;
        private readonly IApplicationUserService applicationUserService;

        public TransactionsController(ITransactionService _transactionService, IApplicationUserService _applicationUserService)
        {
            transactionService = _transactionService;
            applicationUserService = _applicationUserService;
        }

        protected string UserId => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var model = await transactionService.GetAllByUserIdAsync(this.UserId);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddTransactionViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                //find sender by username + id
                //find reciever by username + id
                //check if quantity is valid and in demand
                await transactionService.AddAsync(new TransactionViewModel
                {

                });

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");

                return View(model);
            }
        }
    }
}
