using Library.Core.Contracts;
using Library.Core.Models.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Controllers
{
    public class BooksController : BaseController
    {
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

        protected string UserId => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

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
                string imgUniqueFileName = null;
                string pdfUniqueFileName = null;
                if (model.Image != null)
                {
                    if (!bookService.IsImage(model.Image))
                    {
                        model.Categories = await categoryService.GetCategoriesAsync();

                        ViewBag.ErrorMessage = "Please select a valid image file";

                        return View(model);
                    }

                    if (!bookService.IsPdf(model.Pdf))
                    {
                        model.Categories = await categoryService.GetCategoriesAsync();

                        ViewBag.ErrorMessage = "Please select a valid pdf file";

                        return View(model);
                    }

                    string imgUploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "img/bookcovers");
                    imgUniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string imgFilePath = Path.Combine(imgUploadsFolder, imgUniqueFileName);
                    model.Image.CopyTo(new FileStream(imgFilePath, FileMode.Create));


                    string pdfUploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "pdfs");
                    pdfUniqueFileName = Guid.NewGuid().ToString() + "_" + model.Pdf.FileName;
                    string pdfFilePath = Path.Combine(pdfUploadsFolder, pdfUniqueFileName);
                    model.Pdf.CopyTo(new FileStream(pdfFilePath, FileMode.Create));
                }

                var bookToAdd = new AddBookViewModel
                {
                    Title = model.Title,
                    Description = model.Description,
                    ImageUrl = imgUniqueFileName,
                    PdfUrl = pdfUniqueFileName,
                    CategoryId = model.CategoryId
                };

                await bookService.AddBookAsync(bookToAdd, this.UserId);

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
            var model = await bookService.GetBooksByUserIdAsync(this.UserId);

            return View("Mine", model);
        }

        public async Task<IActionResult> Favorites()
        {
            var model = await bookService.GetFavoritesByUserIdAsync(this.UserId);

            return View("Favorites", model);
        }

        public async Task<IActionResult> Finished()
        {
            var model = await bookService.GetFinishedByUserIdAsync(this.UserId);

            return View("Finished", model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int bookId)
        {
            var model = await bookService.GetByIdAsync(bookId);

            if (model == null)
            {
                return this.NotFound();
            }

            ViewBag.IsInFavorites = await bookService.IsInFavorites(this.UserId, bookId);

            ViewBag.IsInFinished = await bookService.IsInFinished(this.UserId, bookId);

            var ownedBooks = await bookService.GetBooksByUserIdAsync(this.UserId);
            if (ownedBooks.Any(b => b.Id == model.Id))
            {
                ViewBag.IsMine = true;
            }
            else
            {
                ViewBag.IsMine = false;
            }

            return View("Details", model);
        }

        public async Task<IActionResult> FinishBook(int bookId)//
        {
            await bookService.AddToFinishedAsync(bookId, this.UserId);

            //recieve credits:
            await applicationUserService.AddCredits(this.UserId, 5);////////

            //show alert
            TempData["alertMessage"] = "You have successfully finished a book and you recive 5 credits!";

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> AddToCollection(int bookId)//
        {
            if (!await bookService.IsInFavorites(this.UserId, bookId))
            {
                await bookService.AddToFavoritesAsync(bookId, this.UserId);
            }

            return RedirectToAction(nameof(Favorites));
        }

        public async Task<IActionResult> RemoveFromCollection(int bookId)
        {
            await bookService.RemoveFromFavoritesAsync(bookId, this.UserId);

            return RedirectToAction(nameof(Favorites));
        }

        public async Task<IActionResult> Delete(int bookId)
        {
            if (await bookService.IsInFavorites(this.UserId, bookId))
            {
                await bookService.RemoveFromFavoritesAsync(bookId, this.UserId);
            }

            await bookService.DeleteById(bookId);

            return RedirectToAction(nameof(All));
        }

        public FileResult Download(string fileName)
        {
            var FileVirtualPath = "~/pdfs/" + fileName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }
    }
}