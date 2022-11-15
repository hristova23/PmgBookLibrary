using Library.Core.Contracts;
using Library.Core.Models.Transaction;
using Library.Infrastructure.Data.Common;
using Library.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository repo;

        public TransactionService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<int> AddAsync(TransactionViewModel transaction)
        {
            var newTransaction = new Transaction()
            {
                Id = transaction.Id,
                Quantity = transaction.Quantity,
                Message = transaction.Message,
                SenderId = transaction.SenderId,
                RecieverId = transaction.RecieverId,
                Date = DateTime.UtcNow
            };

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
                    Id = t.Id,
                    SenderId = t.SenderId,
                    SenderUsername = t.Sender.UserName,
                    RecieverId = t.RecieverId,
                    RecieverUsername = t.Reciever.UserName,
                    Message = t.Message, //SanitizedMessage?
                    Quantity = t.Quantity,
                    Date = t.Date
                })
                .ToListAsync();
        }

        public async Task<TransactionViewModel> FindByIdAsync(int transactionId)
        {
            var transaction = await repo.All<Transaction>()
                .Where(t => t.Id == transactionId)
                .Include(t => t.Sender)
                .Include(t => t.Reciever)
                .FirstOrDefaultAsync();

            return new TransactionViewModel()
            {
                Id = transaction.Id,
                SenderId = transaction.SenderId,
                SenderUsername = transaction.Sender.UserName,
                RecieverId = transaction.RecieverId,
                RecieverUsername = transaction.Reciever.UserName,
                Message = transaction.Message, //SanitizedMessage?
                Quantity = transaction.Quantity,
                Date = transaction.Date
            };
        }
    }
}