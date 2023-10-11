using Microsoft.AspNetCore.Mvc;

namespace Homestay_Management.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }
    }
}
