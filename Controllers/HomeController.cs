using AirBB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Controllers
{
    public class HomeController : Controller
    {
        private readonly AirBnbContext _context;

        public HomeController(AirBnbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var residences = _context.Residences
                .Include(r => r.Location)
                .ToList();

            return View(residences);
        }
    }
}
