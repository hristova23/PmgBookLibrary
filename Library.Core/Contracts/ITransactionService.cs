using Library.Core.Models.Transaction;

namespace Library.Core.Contracts
{
    public interface ITransactionService
    {
        Task<int> AddAsync(TransactionViewModel transaction);
        Task<IEnumerable<TransactionViewModel>> GetAllByUserIdAsync(string userId);
        Task<TransactionViewModel> FindByIdAsync(int transactionId);
    }
}
