using Library.Core.Contracts;
using Library.Core.Models.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Controllers
{
    public class BooksController : BaseController
    {
        //make property userId!
        private readonly IWebHostEnvironment hostingEnviroment;
        private readonly IBookService bookService;
        private readonly ICategoryService categoryService;
        private readonly IApplicationUserService applicationUserService;

        public BooksController(IWebHostEnvironment _hostingEnviroment, IBookService _bookService, ICategoryService _categoryService, IApplicationUserService _applicationUserService)
        {
            hostingEnviroment = _hostingEnviroment;
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
            var model = new UploadBookViewModel()
            {
                Categories = await categoryService.GetCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(UploadBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.GetCategoriesAsync();

                return View(model);
            }

            try
            {
                string uniqueFileName = null;
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "img/bookcovers");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                var bookToAdd = new AddBookViewModel
                {
                    Title = model.Title,
                    Description = model.Description,
                    ImageUrl = uniqueFileName,
                    CategoryId = model.CategoryId
                };

                await bookService.AddBookAsync(bookToAdd, userId);

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");

                return View(model);
            }
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

        public async Task<IActionResult> Finished()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var model = await bookService.GetFinishedByUserIdAsync(userId);

            return View("Finished", model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int bookId)//bool isInFavorites
        {
            var model = await bookService.GetByIdAsync(bookId);

            if (model == null)
            {
                return this.NotFound();
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            ViewBag.IsInFavorites = await bookService.IsInFavorites(userId, bookId);

            return View("Details", model);
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
        //[HttpPost]//?
        public async Task<IActionResult> RemoveFromCollection(int bookId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await bookService.RemoveBookFromCollectionAsync(bookId, userId);

            return RedirectToAction(nameof(Favorites));
        }

        public async Task<IActionResult> Delete(int bookId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            //The DELETE statement conflicted with the REFERENCE
            //constraint "FK_FavoriteBook_Books_BookId".
            //The conflict occurred in database "PmgBookLibrary",
            //table "dbo.FavoriteBook", column 'BookId'

            await bookService.DeleteById(bookId);

            return RedirectToAction(nameof(Mine));
        }
    }
}