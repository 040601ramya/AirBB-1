using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

using AirBB.Models.DataLayer;        
using AirBB.Models.DomainModels;       
using System.IO;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ResidenceController : Controller
    {
        private readonly AirBnbContext _context;
        private readonly IWebHostEnvironment _env;

        public ResidenceController(AirBnbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var residences = await _context.Residences
                .Include(r => r.Location)
                .ToListAsync();

            return View(residences);
        }

        public IActionResult Create()
        {
            ViewBag.Locations = _context.Locations.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Residence model, IFormFile picture)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Locations = _context.Locations.ToList();
                return View(model);
            }

            if (picture != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);
                string path = Path.Combine(_env.WebRootPath, "uploads", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await picture.CopyToAsync(stream);
                }

                model.ResidencePicture = fileName;
            }

            _context.Residences.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var residence = await _context.Residences.FindAsync(id);
            if (residence == null) return NotFound();

            ViewBag.Locations = _context.Locations.ToList();
            return View(residence);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Residence model, IFormFile picture)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Locations = _context.Locations.ToList();
                return View(model);
            }

            if (picture != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);
                string path = Path.Combine(_env.WebRootPath, "uploads", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await picture.CopyToAsync(stream);
                }

                model.ResidencePicture = fileName;
            }

            _context.Residences.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var residence = await _context.Residences
                .Include(r => r.Location)
                .FirstOrDefaultAsync(r => r.ResidenceId == id);

            if (residence == null) return NotFound();

            return View(residence);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int residenceId)
        {
            var residence = await _context.Residences.FindAsync(residenceId);
            if (residence == null) return NotFound();

            _context.Residences.Remove(residence);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        
        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyOwner(int ownerId)
        {
            bool exists = _context.Users.Any(u => u.UserId == ownerId);

            if (exists)
            {
                return Json(true);
            }

            return Json($"Owner with id {ownerId} does not exist.");
        }
    }
}
