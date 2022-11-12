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

        public async Task<bool> BookExists(int bookId)
        {
            return await repo.AllReadonly<Book>()
                .AnyAsync(c => c.Id == bookId);
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

        public async Task<IEnumerable<BookViewModel>> GetAllAsync()
        {
            return await repo.AllReadonly<Book>()
                .OrderByDescending(b => b.Id)
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
            throw new NotImplementedException();
        }

        public async Task<int> DeleteBookAsync(int bookId)
        {
            //check if book exists
            await repo.DeleteAsync<Book>(bookId);
            await repo.SaveChangesAsync();

            return bookId;
        }


        //public async Task AddBookToCollectionAsync(int bookId, string userId)
        //{
        //    var user = await context.Users
        //        .Where(u => u.Id == userId)
        //        .Include(u => u.ApplicationUsersBooks)
        //        .FirstOrDefaultAsync();

        //    if (user == null)
        //    {
        //        throw new ArgumentException("Invalid user ID");
        //    }

        //    var book = await context.Books.FirstOrDefaultAsync(u => u.Id == bookId);

        //    if (book == null)
        //    {
        //        throw new ArgumentException("Invalid Book ID");
        //    }

        //    if (!user.ApplicationUsersBooks.Any(m => m.BookId == bookId))
        //    {
        //        user.ApplicationUsersBooks.Add(new ApplicationUserBook()
        //        {
        //            BookId = book.Id,
        //            ApplicationUserId = user.Id,
        //            Book = book,
        //            ApplicationUser = user
        //        });

        //        await context.SaveChangesAsync();
        //    }
        //}
    }
}