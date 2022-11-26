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

            string senderEmail = applicationUserService.GetUserByIdAsync(this.UserId).Result.Email;
            string recieverId = applicationUserService.GetIdByEmailAsync(model.RecieverEmail).Result;

            await transactionService.AddAsync(new TransactionViewModel
            {
                SenderEmail = senderEmail,
                RecieverEmail = model.RecieverEmail,
                Quantity = model.Quantity,
                Message = model.SanitizedMessage,
                Date = DateTime.UtcNow
            }, this.UserId, recieverId);

            return RedirectToAction("All");
        }

        public async Task<IActionResult> Details(int transactionId)
        {
            var model = await transactionService.GetByIdAsync(transactionId);

            if (model == null)
            {
                return this.NotFound();
            }

            return View("Details", model);
        }
    }
}
