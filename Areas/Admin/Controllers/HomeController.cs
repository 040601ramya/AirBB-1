using Microsoft.AspNetCore.Mvc;
using AirBB.Models.DataLayer;      
using AirBB.Models.DomainModels;   

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
