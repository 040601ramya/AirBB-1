using Microsoft.AspNetCore.Mvc;

namespace AirBB.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index() => View();
    }
}
