using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AirBB.Models.DomainModels;            
using AirBB.Models.DataLayer;               
using AirBB.Models.DataLayer.Repositories;  

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly AirBnbContext _context;

        public UserController(AirBnbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.Users
                .OrderBy(u => u.Name)
                .ToListAsync();

            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User model)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User model)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
