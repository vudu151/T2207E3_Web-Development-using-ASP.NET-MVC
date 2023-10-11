using Microsoft.AspNetCore.Mvc;

namespace Homestay_Management.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult News()
        {
            return View();
        }


        public IActionResult NewsDetail()
        {
            return View();
        }
    }
}
