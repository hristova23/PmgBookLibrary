using Library.Core.Contracts;
using Library.Core.Models.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Controllers
{
    public class BooksController : BaseController
    {
        private readonly IBookService bookService;
        private readonly ICategoryService categoryService;
        private readonly IApplicationUserService applicationUserService;

        public BooksController(IBookService _bookService, ICategoryService _categoryService, IApplicationUserService _applicationUserService)
        {
            bookService = _bookService;
            categoryService = _categoryService;
            applicationUserService = _applicationUserService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var model = await bookService.GetAllAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddBookViewModel()
            {
                Categories = await categoryService.GetCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.GetCategoriesAsync();

                return View(model);
            }

            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                await bookService.AddBookAsync(model, userId);

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");

                return View(model);
            }
        }

        public async Task<IActionResult> AddToCollection(int bookId)
        {
            if (!ModelState.IsValid)
            {
                return View(bookId);
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!await bookService.ExistsById(bookId))
            {
                //Add error message

                return View(bookId);
            }

            if (!await applicationUserService.ExistsById(userId))//is necessary??
            {
                //Add error message

                return View(bookId);
            }

            if (!await bookService.IsInFavorites(userId, bookId))
            {
                await bookService.AddBookToCollectionAsync(bookId, userId);
            }

            return RedirectToAction(nameof(Favorites));
        }
        
        public async Task<IActionResult> Mine()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var model = await bookService.GetBooksByUserIdAsync(userId);

            return View("Mine", model);
        }

        public async Task<IActionResult> Favorites()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var model = await bookService.GetFavoritesByUserIdAsync(userId);

            return View("Favorites", model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int bookId)
        {
            var model = await bookService.GetByIdAsync(bookId);

            if (model == null)
            {
                return this.NotFound();
            }

            return View("Details", model);
        }

        //[HttpPost]//?
        public async Task<IActionResult> RemoveFromCollection(int bookId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await bookService.RemoveBookFromCollectionAsync(bookId, userId);

            return RedirectToAction(nameof(All));
        }
    }
}
