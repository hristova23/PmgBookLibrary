using Library.Core.Models.Transaction;

namespace Library.Core.Contracts
{
    public interface ITransactionService
    {
        Task<int> AddAsync(TransactionViewModel transaction, string senderId, string recieverId);
        Task<IEnumerable<TransactionViewModel>> GetAllByUserIdAsync(string userId);
        Task<TransactionViewModel> GetByIdAsync(int transactionId);
    }
}
