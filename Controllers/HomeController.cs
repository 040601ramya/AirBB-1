using Microsoft.AspNetCore.Mvc;
using AirBB.Models.DomainModels;                 
using AirBB.Models.DataLayer;                  
using AirBB.Models.DataLayer.Repositories;      

namespace AirBB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Residence> _resRepo;

        public HomeController(IRepository<Residence> resRepo)
        {
            _resRepo = resRepo;
        }

        public async Task<IActionResult> Index()
        {
            
            var options = new QueryOptions<Residence>
            {
                OrderBy = r => r.Name   
            };

            options.Includes.Add(r => r.Location);

           
            var residences = await _resRepo.ListAsync(options);

            return View(residences);
        }
    }
}
