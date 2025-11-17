using AirBB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Controllers
{
    public class ReservationController : Controller
    {
        private readonly AirBnbContext _context;

        public ReservationController(AirBnbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var reservations = await _context.Reservations
                .Include(r => r.Residence)
                .Include(r => r.Client)
                .ToListAsync();

            return View(reservations);
        }

       
        public IActionResult Create()
        {
            ViewBag.Residences = _context.Residences.ToList();
            ViewBag.Clients = _context.Clients.ToList();
            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                
                var residence = await _context.Residences.FindAsync(reservation.ResidenceId);
                var nights = (reservation.ReservationEndDate - reservation.ReservationStartDate).Days;

                reservation.TotalPrice = residence.PricePerNight * nights;

                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Residences = _context.Residences.ToList();
            ViewBag.Clients = _context.Clients.ToList();
            return View(reservation);
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return NotFound();

            ViewBag.Residences = _context.Residences.ToList();
            ViewBag.Clients = _context.Clients.ToList();
            return View(reservation);
        }

        
        [HttpPost]
        public async Task<IActionResult> Edit(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var residence = await _context.Residences.FindAsync(reservation.ResidenceId);
                var nights = (reservation.ReservationEndDate - reservation.ReservationStartDate).Days;

                reservation.TotalPrice = residence.PricePerNight * nights;

                _context.Reservations.Update(reservation);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Residences = _context.Residences.ToList();
            ViewBag.Clients = _context.Clients.ToList();
            return View(reservation);
        }

      
        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Residence)
                .Include(r => r.Client)
                .FirstOrDefaultAsync(r => r.ReservationId == id);

            if (reservation == null) return NotFound();

            return View(reservation);
        }

       
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return NotFound();

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Details(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Residence)
                .Include(r => r.Client)
                .FirstOrDefaultAsync(r => r.ReservationId == id);

            if (reservation == null) return NotFound();

            return View(reservation);
        }
    }
}
