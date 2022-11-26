using Library.Core.Contracts;
using Library.Core.Models.Book;
using Library.Infrastructure.Data.Common;
using Library.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository repo;

        public BookService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<bool> ExistsById(int bookId)
        {
            return await repo.AllReadonly<Book>()
                .AnyAsync(c => c.Id == bookId);
        }

        public async Task<bool> IsInFavorites(string userId, int bookId)
        {
            return await repo.AllReadonly<FavoriteBook>()
                .AnyAsync(c => c.UserId == userId && c.BookId == bookId);
        }

        public async Task<int> AddBookAsync(AddBookViewModel model, string userId)
        {
            var book = new Book()
            {
                Title = model.Title,
                PublisherId = userId,
                CategoryId = model.CategoryId,
                Description = model.Description,
                ImageUrl = model.ImageUrl
            };

            await repo.AddAsync(book);
            await repo.SaveChangesAsync();

            return book.Id;
        }

        public async Task<BookViewModel> GetByIdAsync(int bookId)
        {
            var book = await repo.All<Book>()
                .Where(b => b.Id == bookId)
                .Include(b => b.Publisher)
                .Include(b => b.Category)
                .FirstOrDefaultAsync();

            //var book = await repo.GetByIdAsync<Book>(bookId);
            //var publisher = await repo.GetByIdAsync<ApplicationUser>(book.PublisherId);
            //var category = await repo.GetByIdAsync<Category>(book.CategoryId);

            return new BookViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                Publisher = book.Publisher.UserName,
                Category = book.Category.Name,
                Description = book.Description,
                ImageUrl = book.ImageUrl
            };
        }

        public async Task<IEnumerable<BookViewModel>> GetAllAsync()
        {
            return await repo.AllReadonly<Book>()
                //.OrderByDescending(b => b.Id)
                .Select(b => new BookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Publisher = b.Publisher.UserName,
                    Category = b.Category.Name,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<BookViewModel>> GetBooksByUserIdAsync(string userId)
        {
            return await repo.AllReadonly<Book>()
                .Where(a => a.PublisherId == userId)
                .Select(b => new BookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Publisher = b.Publisher.UserName,
                    Category = b.Category.Name,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<BookViewModel>> GetFavoritesByUserIdAsync(string userId)
        {
            var user = await repo.All<ApplicationUser>()
                .Where(u => u.Id == userId)
                .Include(u => u.FavoriteBooks)
                .ThenInclude(fb => fb.Book)
                .ThenInclude(b => b.Category)
                .Include(u => u.FavoriteBooks)
                .ThenInclude(fb => fb.Book)
                .ThenInclude(b => b.Publisher)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            return user.FavoriteBooks
                .Select(b => new BookViewModel()
                {
                    Id = b.BookId,
                    Title = b.Book.Title,
                    Publisher = b.Book.Publisher.UserName,
                    Category = b.Book.Category.Name,
                    Description = b.Book.Description,
                    ImageUrl = b.Book.ImageUrl,
                });
        }

        public async Task<IEnumerable<BookViewModel>> GetFinishedByUserIdAsync(string userId)
        {
            var user = await repo.All<ApplicationUser>()
                .Where(u => u.Id == userId)
                .Include(u => u.FinishedBooks)
                .ThenInclude(fb => fb.Book)
                .ThenInclude(b => b.Category)
                .Include(u => u.FinishedBooks)
                .ThenInclude(fb => fb.Book)
                .ThenInclude(b => b.Publisher)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            return user.FinishedBooks
                .Select(b => new BookViewModel()
                {
                    Id = b.BookId,
                    Title = b.Book.Title,
                    Publisher = b.Book.Publisher.UserName,
                    Category = b.Book.Category.Name,
                    Description = b.Book.Description,
                    ImageUrl = b.Book.ImageUrl,
                });
        }

        public async Task AddBookToCollectionAsync(int bookId, string userId)
        {
            var book = new FavoriteBook()
            {
                UserId = userId,
                BookId = bookId
            };

            await repo.AddAsync(book);
            await repo.SaveChangesAsync();
        }

        public async Task RemoveBookFromCollectionAsync(int bookId, string userId)
        {
            var recordToRemove = await repo.All<FavoriteBook>()
                .Where(u => u.UserId == userId && u.BookId == bookId)
                .FirstOrDefaultAsync();

            repo.Delete<FavoriteBook>(recordToRemove);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteById(int bookId)
        {
            await repo.DeleteAsync<Book>(bookId);
            await repo.SaveChangesAsync();
        }
    }
}