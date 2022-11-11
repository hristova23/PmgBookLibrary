using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
