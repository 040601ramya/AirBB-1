using Microsoft.AspNetCore.Mvc;
using AirBB.Models.DomainModels;                 
using AirBB.Models.DataLayer;                   
using AirBB.Models.DataLayer.Repositories;      

namespace AirBB.Controllers
{
    public class ExperiencesController : Controller
    {
        private readonly IRepository<Experience> _repo;

        public ExperiencesController(IRepository<Experience> repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var options = new QueryOptions<Experience>
            {
                OrderBy = e => e.ExperienceId
            };

            var experiences = await _repo.ListAsync(options);
            return View(experiences);
        }

  
        public async Task<IActionResult> Details(int id)
        {
            var options = new QueryOptions<Experience>
            {
                Where = e => e.ExperienceId == id
            };

            var experience = await _repo.GetAsync(options);

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
                await _repo.InsertAsync(experience);
                await _repo.SaveAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(experience);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var experience = await _repo.GetAsync(id);

            if (experience == null)
                return NotFound();

            return View(experience);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Experience experience)
        {
            if (ModelState.IsValid)
            {
                await _repo.UpdateAsync(experience);
                await _repo.SaveAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(experience);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var experience = await _repo.GetAsync(id);

            if (experience == null)
                return NotFound();

            return View(experience);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var experience = await _repo.GetAsync(id);

            if (experience == null)
                return NotFound();

            await _repo.DeleteAsync(experience);
            await _repo.SaveAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
