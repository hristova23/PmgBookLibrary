using Library.Core.Models.Book;
using Microsoft.AspNetCore.Http;

namespace Library.Core.Contracts
{
    public interface IBookService
    {
        Task<bool> ExistsById(int bookId);

        Task<BookViewModel> GetByIdAsync(int bookId);

        Task<bool> IsInFavorites(string userId, int bookId);

        Task<bool> IsInFinished(string userId, int bookId);

        Task<int> AddBookAsync(AddBookViewModel model, string userId);

        Task<IEnumerable<BookViewModel>> GetAllAsync();

        Task<IEnumerable<BookViewModel>> GetBooksByUserIdAsync(string userId);

        Task<IEnumerable<BookViewModel>> GetFavoritesByUserIdAsync(string userId);

        Task<IEnumerable<BookViewModel>> GetFinishedByUserIdAsync(string userId);

        Task AddToFinishedAsync(int bookId, string userId);

        Task AddToFavoritesAsync(int bookId, string userId);

        Task RemoveFromFavoritesAsync(int bookId, string userId);

        Task DeleteById(int bookId);

        bool IsImage(IFormFile postedFile);

        bool IsPdf(IFormFile postedFile);

    }
}
