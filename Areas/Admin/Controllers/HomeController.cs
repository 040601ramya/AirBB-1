using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            ViewBag.TotalUsers = _context.Users.Count();
            ViewBag.TotalResidence = _context.Residences.Count();   // FIXED
            ViewBag.TotalReservations = _context.Reservations.Count();
            ViewBag.TotalLocations = _context.Locations.Count();

            return View();
        }
    }
}
