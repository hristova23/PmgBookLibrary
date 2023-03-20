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
        public IActionResult Add()
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

            var sender = await applicationUserService.GetByIdAsync(this.UserId);
            var reciever = await applicationUserService.GetByEmailAsync(model.RecieverEmail);

            if (reciever == null)
            {
                ViewBag.ErrorMessage = "There is no user associated with this email";

                return View(model);
            }

            if (sender.Email == reciever.Email)
            {
                ViewBag.ErrorMessage = "You can't send credits to yourself!";

                return View(model);
            }

            if (model.Quantity > sender.Credits)
            {
                ViewBag.ErrorMessage = $"Cannot send {model.Quantity} credits. You only have {sender.Credits}";

                return View(model);
            }

            await transactionService.AddAsync(new TransactionViewModel
            {
                SenderEmail = sender.Email,
                RecieverEmail = model.RecieverEmail,
                Quantity = model.Quantity,
                Message = model.SanitizedMessage,
                Date = DateTime.UtcNow
            }, this.UserId, reciever.Id);

            return RedirectToAction("All");
        }
    }
}
