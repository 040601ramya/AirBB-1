using AirBB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Controllers
{
    public class ExperiencesController : Controller
    {
        private readonly AirBnbContext _context;

        public ExperiencesController(AirBnbContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {
            var experiences = await _context.Experiences.ToListAsync();
            return View(experiences);
        }

       
        public async Task<IActionResult> Details(int id)
        {
            var experience = await _context.Experiences
                .FirstOrDefaultAsync(e => e.ExperienceId == id);

            if (experience == null)
                return NotFound();

            return View(experience);
        }

        
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> Create(Experience experience)
        {
            if (ModelState.IsValid)
            {
                _context.Experiences.Add(experience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(experience);
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);
            if (experience == null)
                return NotFound();

            return View(experience);
        }

       
        [HttpPost]
        public async Task<IActionResult> Edit(Experience experience)
        {
            if (ModelState.IsValid)
            {
                _context.Experiences.Update(experience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(experience);
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            var experience = await _context.Experiences
                .FirstOrDefaultAsync(e => e.ExperienceId == id);

            if (experience == null)
                return NotFound();

            return View(experience);
        }

       
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);

            if (experience == null)
                return NotFound();

            _context.Experiences.Remove(experience);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
