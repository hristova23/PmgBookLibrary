using Library.Core.Contracts;
using Library.Core.Models.Transaction;
using Library.Infrastructure.Data.Common;
using Library.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository repo;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly SignInManager<ApplicationUser> signInManager;

        public TransactionService(IRepository _repo,
            UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager)
        {
            repo = _repo;
            userManager = _userManager;
            signInManager = _signInManager;
        }

        public async Task<int> AddAsync(AddTransactionViewModel transaction, string senderId)
        {
            ApplicationUser sender = await repo.GetByIdAsync<ApplicationUser>(senderId);//check if null
            ApplicationUser reciever = await repo.GetByIdAsync<ApplicationUser>(transaction.RecieverId);//check if null

            var newTransaction = new Transaction()
            {
                Id = transaction.Id,
                Quantity = transaction.Quantity,
                Message = transaction.Message,
                SenderId = senderId,
                RecieverId = transaction.RecieverId,
                Date = DateTime.UtcNow
            };

            sender.Credits -= transaction.Quantity;
            reciever.Credits += transaction.Quantity;

            await repo.AddAsync(newTransaction);
            await repo.SaveChangesAsync();

            return newTransaction.Id;
        }

        public async Task<IEnumerable<TransactionViewModel>> GetAllByUserIdAsync(string userId)
        {
            return await repo.AllReadonly<Transaction>()
                .Where(t => t.SenderId == userId || t.RecieverId == userId)
                .Select(t => new TransactionViewModel()
                {
                    SenderEmail = t.Sender.Email,
                    RecieverEmail = t.Reciever.Email,
                    Message = t.Message, //SanitizedMessage?
                    Quantity = t.Quantity,
                    Date = t.Date
                })
                .ToListAsync();
        }

        public async Task<TransactionViewModel> GetByIdAsync(int transactionId)
        {
            var transaction = await repo.All<Transaction>()
                .Where(t => t.Id == transactionId)
                .Include(t => t.Sender)
                .Include(t => t.Reciever)
                .FirstOrDefaultAsync();

            //var transaction = await repo.GetByIdAsync<Transaction>(transactionId);

            return new TransactionViewModel()
            {
                SenderEmail = transaction.Sender.Email,
                RecieverEmail = transaction.Reciever.Email,
                Message = transaction.Message, //SanitizedMessage?
                Quantity = transaction.Quantity,
                Date = transaction.Date
            };
        }
    }
}