using Microsoft.AspNetCore.Mvc;
using AirBB.Models.DomainModels;
using AirBB.Models.DataLayer;
using AirBB.Models.DataLayer.Repositories;

namespace AirBB.Controllers
{
    public class ResidencesController : Controller
    {
        private readonly IRepository<Residence> _resRepo;

        public ResidencesController(IRepository<Residence> resRepo)
        {
            _resRepo = resRepo;
        }

        public async Task<IActionResult> Details(int id)
        {
            var options = new QueryOptions<Residence>
            {
                Where = r => r.ResidenceId == id
            };

            options.Includes.Add(r => r.Location);

            var residence = await _resRepo.GetAsync(options);

            if (residence == null)
                return NotFound();

            return View(residence!);  
        }
    }
}
