using Microsoft.AspNetCore.Mvc;
using AirBB.Models;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly AirBnbContext _context;

        public HomeController(AirBnbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
