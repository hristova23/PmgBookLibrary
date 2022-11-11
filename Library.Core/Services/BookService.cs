using Library.Core.Contracts;
using Library.Core.Models;//
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

        public Task AddBookAsync(AddBookViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task AddBookToCollectionAsync(int bookId, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BookViewModel>> GetAllAsync()
        {
            return await repo.AllReadonly<Book>()
                .OrderByDescending(b => b.Id)
                .Select(b => new BookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl
                })
                .ToListAsync();
        }

        public Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync()
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

        //public async Task AddBookAsync(AddBookViewModel model)
        //{
        //    ApplicationUser author = await context.Users.FindAsync(); 

        //    var entity = new Book()
        //    {
        //        AuthorId = model.Author.Id,
        //        CategoryId = model.CategoryId,
        //        Description = model.Description,
        //        ImageUrl = model.ImageUrl,
        //        Rating = model.Rating,
        //        Title = model.Title
        //    };

        //    await context.Books.AddAsync(entity);
        //    await context.SaveChangesAsync();
        //}

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

        //public async Task<IEnumerable<BookViewModel>> GetAllAsync()
        //{
        //    var entities = await context.Books
        //        .Include(m => m.Category)
        //        .ToListAsync();

        //    return entities
        //        .Select(m => new BookViewModel()
        //        {
        //            Author = m.Author,
        //            Category = m.Category.Name,
        //            Id = m.Id,
        //            ImageUrl = m.ImageUrl,
        //            Rating = m.Rating,
        //            Title = m.Title
        //        });
        //}

        //public async Task<IEnumerable<Category>> GetCategoriesAsync()
        //{
        //    return await context.Categories.ToListAsync();
        //}

        //public async Task<IEnumerable<BookViewModel>> GetMyBooksAsync(string userId)
        //{
        //    var user = await context.Users
        //        .Where(u => u.Id == userId)
        //        .Include(u => u.ApplicationUsersBooks)
        //        .ThenInclude(um => um.Book)
        //        .ThenInclude(m => m.Category)
        //        .FirstOrDefaultAsync();

        //    if (user == null)
        //    {
        //        throw new ArgumentException("Invalid user ID");
        //    }

        //    return user.ApplicationUsersBooks
        //        .Select(m => new BookViewModel()
        //        {
        //            Author = m.Book.Author,
        //            Category = m.Book.Category.Name,
        //            Description = m.Book.Description,
        //            Id = m.BookId,
        //            ImageUrl = m.Book.ImageUrl,
        //            Title = m.Book.Title,
        //            Rating = m.Book.Rating,
        //        });
        //}

        //public async Task RemoveBookFromCollectionAsync(int bookId, string userId)
        //{
        //    var user = await context.Users
        //        .Where(u => u.Id == userId)
        //        .Include(u => u.ApplicationUsersBooks)
        //        .FirstOrDefaultAsync();

        //    if (user == null)
        //    {
        //        throw new ArgumentException("Invalid user ID");
        //    }

        //    var book = user.ApplicationUsersBooks.FirstOrDefault(m => m.BookId == bookId);

        //    if (book != null)
        //    {
        //        user.ApplicationUsersBooks.Remove(book);

        //        await context.SaveChangesAsync();
        //    }
        //}
    }
}
