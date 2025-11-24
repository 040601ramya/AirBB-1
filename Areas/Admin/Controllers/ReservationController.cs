using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirBB.Models.DataLayer;         
using AirBB.Models.DomainModels;      

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReservationController : Controller
    {
        private readonly AirBnbContext _context;

        public ReservationController(AirBnbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var list = _context.Reservations
                .Include(r => r.Residence)
                .Include(r => r.Client)
                .ToList();

            return View(list);
        }

        public IActionResult Details(int id)
        {
            var reservation = _context.Reservations
                .Include(r => r.Residence)
                .Include(r => r.Client)
                .FirstOrDefault(r => r.ReservationId == id);

            return reservation == null ? NotFound() : View(reservation);
        }

        public IActionResult Delete(int id)
        {
            var reservation = _context.Reservations
                .Include(r => r.Residence)
                .Include(r => r.Client)
                .FirstOrDefault(r => r.ReservationId == id);

            return reservation == null ? NotFound() : View(reservation);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int reservationId)
        {
            var res = _context.Reservations.Find(reservationId);

            if (res != null)
            {
                _context.Reservations.Remove(res);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
