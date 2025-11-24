using Microsoft.AspNetCore.Mvc;
using AirBB.Models.DataLayer;        
using AirBB.Models.DomainModels;     
namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LocationsController : Controller
    {
        private readonly AirBnbContext _context;

        public LocationsController(AirBnbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var locations = _context.Locations.ToList();
            return View(locations);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Location model)
        {
            if (ModelState.IsValid)
            {
                _context.Locations.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var location = _context.Locations.Find(id);
            if (location == null) return NotFound();
            return View(location);
        }

        [HttpPost]
        public IActionResult Edit(Location model)
        {
            if (ModelState.IsValid)
            {
                _context.Locations.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var location = _context.Locations.Find(id);
            if (location == null) return NotFound();
            return View(location);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var location = _context.Locations.Find(id);
            if (location == null) return NotFound();

            _context.Locations.Remove(location);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
