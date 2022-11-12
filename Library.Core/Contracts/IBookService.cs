using Library.Core.Models.Book;

namespace Library.Core.Contracts
{
    public interface IBookService
    {
        Task<bool> BookExists(int bookId);

        Task<int> AddBookAsync(AddBookViewModel model, string userId);

        Task<IEnumerable<BookViewModel>> GetAllAsync();

        Task<IEnumerable<BookViewModel>> GetBooksByUserIdAsync(string userId);

        Task AddBookToCollectionAsync(int bookId, string userId);

        Task RemoveBookFromCollectionAsync(int bookId, string userId);

        Task<int> DeleteBookAsync(int bookId);
    }
}
