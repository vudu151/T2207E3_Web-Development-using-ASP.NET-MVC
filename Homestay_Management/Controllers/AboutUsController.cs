using Microsoft.AspNetCore.Mvc;

namespace Homestay_Management.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
