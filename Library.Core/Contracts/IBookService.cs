using Library.Core.Models.Book;

namespace Library.Core.Contracts
{
    public interface IBookService
    {
        Task<bool> ExistsById(int bookId);

        Task<bool> IsInFavorites(string userId, int bookId);

        Task<int> AddBookAsync(AddBookViewModel model, string userId);

        Task<IEnumerable<BookViewModel>> GetAllAsync();

        Task<IEnumerable<BookViewModel>> GetBooksByUserIdAsync(string userId);

        Task<BookViewModel> GetByIdAsync(int bookId);

        Task<IEnumerable<BookViewModel>> GetFavoritesByUserIdAsync(string userId);

        Task AddBookToCollectionAsync(int bookId, string userId);

        Task RemoveBookFromCollectionAsync(int bookId, string userId);
    }
}
