using Microsoft.AspNetCore.Mvc;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Admin Home";
            return View();
        }
    }
}
