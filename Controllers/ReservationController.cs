using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirBB.Models;
using AirBB.Models.ViewModels;
using AirBB.Models.Cookies;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AirBB.Controllers
{
    public class ReservationController : Controller
    {
        private readonly AirBnbContext _context;
        public ReservationController(AirBnbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var vms = await _context.Reservations
                .Include(r => r.Residence)!.ThenInclude(res => res!.Location)
                .Select(r => new ReservationViewModel
                {
                    ReservationId = r.ReservationId,
                    ResidenceName = r.Residence != null ? r.Residence.Name : "",
                    LocationName = r.Residence != null && r.Residence.Location != null ? r.Residence.Location.Name : "",
                    ReservationStartDate = r.ReservationStartDate,
                    ReservationEndDate = r.ReservationEndDate,
                    TotalPrice = r.TotalPrice
                })
                .ToListAsync();

            return View(vms);
        }

        [HttpGet]
        public IActionResult Reserve() => RedirectToAction("Index", "Home");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve(int residenceId, DateTime startDate, DateTime endDate)
        {
            var s = startDate.Date;
            var e = endDate.Date;
            if (e <= s)
            {
                TempData["ReservationMessage"] = "End date must be after start date.";
                return RedirectToAction("Index", "Home");
            }

            var exists = await _context.Residences.AnyAsync(x => x.ResidenceId == residenceId);
            if (!exists)
            {
                TempData["ReservationMessage"] = "Residence not found.";
                return RedirectToAction("Index", "Home");
            }

            // Half-open overlap: [start, end)
            var overlap = await _context.Reservations.AnyAsync(r =>
                r.ResidenceId == residenceId &&
                r.ReservationStartDate < e &&
                r.ReservationEndDate > s);

            if (overlap)
            {
                TempData["ReservationMessage"] = "That date range is not available.";
                return RedirectToAction("Index", "Home");
            }

            var res = await _context.Residences.FindAsync(residenceId);
            var nights = (e - s).TotalDays;

            var entity = new Reservation
            {
                ResidenceId = residenceId,
                ReservationStartDate = s,
                ReservationEndDate = e,
                TotalPrice = res != null ? (decimal)Math.Max(0, nights) * res.PricePerNight : 0m,
                ClientId = 1
            };

            _context.Reservations.Add(entity);
            await _context.SaveChangesAsync();

            var ids = CookieHelper.GetReservationIds(Request);
            if (!ids.Contains(entity.ReservationId)) ids.Add(entity.ReservationId);
            CookieHelper.SaveReservationIds(Response, ids);

            TempData["ReservationMessage"] = "Reservation confirmed!";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            var entity = await _context.Reservations.FindAsync(id);
            if (entity == null) return NotFound();

            _context.Reservations.Remove(entity);
            await _context.SaveChangesAsync();

            var ids = CookieHelper.GetReservationIds(Request);
            ids.Remove(id);
            CookieHelper.SaveReservationIds(Response, ids);

            TempData["ReservationMessage"] = "Reservation cancelled.";
            return RedirectToAction(nameof(Index));
        }
    }
}
