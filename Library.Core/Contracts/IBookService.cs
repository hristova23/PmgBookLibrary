using Library.Core.Models;//
using Library.Core.Models.Book;

namespace Library.Core.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<BookViewModel>> GetAllAsync();

        Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync();

        Task AddBookAsync(AddBookViewModel model);

        Task AddBookToCollectionAsync(int bookId, string userId);

        Task<IEnumerable<BookViewModel>> GetMyBooksAsync(string userId);

        Task RemoveBookFromCollectionAsync(int bookId, string userId);
    }
}
