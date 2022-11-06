using Library.Contracts;
using Library.Data;
using Library.Data.Models;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext context;

        public BookService(LibraryDbContext _context)
        {
            context = _context;
        }

        public Task AddBookAsync(AddBookViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task AddBookToCollectionAsync(int bookId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookViewModel>> GetMyBooksAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveBookFromCollectionAsync(int bookId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
