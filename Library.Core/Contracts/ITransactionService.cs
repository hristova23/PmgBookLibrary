using Library.Core.Models.Transaction;

namespace Library.Core.Contracts
{
    public interface ITransactionService
    {
        Task<int> AddAsync(AddTransactionViewModel transaction, string senderId);
        Task<IEnumerable<TransactionViewModel>> GetAllByUserIdAsync(string userId);
        Task<TransactionViewModel> GetByIdAsync(int transactionId);
    }
}
