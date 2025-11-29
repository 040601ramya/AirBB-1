using Microsoft.AspNetCore.Mvc;
using AirBB.Models.DomainModels;
using AirBB.Models.DataLayer;
using AirBB.Models.DataLayer.Repositories;

namespace AirBB.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IRepository<Reservation> _resRepo;
        private readonly IRepository<Residence> _residenceRepo;
        private readonly IRepository<Client> _clientRepo;

        public ReservationController(
            IRepository<Reservation> resRepo,
            IRepository<Residence> residenceRepo,
            IRepository<Client> clientRepo)
        {
            _resRepo = resRepo;
            _residenceRepo = residenceRepo;
            _clientRepo = clientRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Reserve(int ResidenceId, DateTime StartDate, DateTime EndDate)
        {
            var residence = await _residenceRepo.GetAsync(ResidenceId);

            if (residence == null)
            {
                TempData["Error"] = "Residence not found.";
                return RedirectToAction("Index", "Home");
            }

          
            if (EndDate <= StartDate)
            {
                TempData["Error"] = "Check-out date must be after check-in.";
                return RedirectToAction("Details", "Residences", new { id = ResidenceId });
            }


            var checkOptions = new QueryOptions<Reservation>
            {
                Where = r =>
                    r.ResidenceId == ResidenceId &&
                    StartDate < r.ReservationEndDate &&
                    EndDate > r.ReservationStartDate
            };

            var existing = await _resRepo.ListAsync(checkOptions);

            if (existing.Any())
            {
                TempData["Error"] = "This residence is already booked for the selected dates.";
                return RedirectToAction("Details", "Residences", new { id = ResidenceId });
            }

            int nights = (EndDate - StartDate).Days;
            if (nights <= 0) nights = 1;

            decimal total = residence.PricePerNight * nights;

            var reservation = new Reservation
            {
                ResidenceId = ResidenceId,
                ClientId = 1, 
                ReservationStartDate = StartDate,
                ReservationEndDate = EndDate,
                TotalPrice = total
            };

            await _resRepo.InsertAsync(reservation);
            await _resRepo.SaveAsync();

            return RedirectToAction(nameof(Details), new { id = reservation.ReservationId });
        }


        public async Task<IActionResult> Index()
        {
            var options = new QueryOptions<Reservation>();
            options.Includes.Add(r => r.Residence);
            options.Includes.Add(r => r.Client);
            options.OrderBy = r => r.ReservationId;

            var reservations = await _resRepo.ListAsync(options);
            return View(reservations!);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.Residences = await _residenceRepo.ListAsync();
            ViewBag.Clients = await _clientRepo.ListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var residence = await _residenceRepo.GetAsync(reservation.ResidenceId);

                if (residence == null)
                {
                    ModelState.AddModelError("", "Selected residence does not exist.");
                    ViewBag.Residences = await _residenceRepo.ListAsync();
                    ViewBag.Clients = await _clientRepo.ListAsync();
                    return View(reservation);
                }

                int nights = (reservation.ReservationEndDate - reservation.ReservationStartDate).Days;
                if (nights <= 0) nights = 1;
                reservation.TotalPrice = residence.PricePerNight * nights;

                await _resRepo.InsertAsync(reservation);
                await _resRepo.SaveAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Residences = await _residenceRepo.ListAsync();
            ViewBag.Clients = await _clientRepo.ListAsync();
            return View(reservation);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var reservation = await _resRepo.GetAsync(id);
            if (reservation == null) return NotFound();

            ViewBag.Residences = await _residenceRepo.ListAsync();
            ViewBag.Clients = await _clientRepo.ListAsync();
            return View(reservation!);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var residence = await _residenceRepo.GetAsync(reservation.ResidenceId);

                if (residence == null)
                {
                    ModelState.AddModelError("", "Selected residence does not exist.");
                    ViewBag.Residences = await _residenceRepo.ListAsync();
                    ViewBag.Clients = await _clientRepo.ListAsync();
                    return View(reservation);
                }

                int nights = (reservation.ReservationEndDate - reservation.ReservationStartDate).Days;
                if (nights <= 0) nights = 1;
                reservation.TotalPrice = residence.PricePerNight * nights;

                await _resRepo.UpdateAsync(reservation);
                await _resRepo.SaveAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Residences = await _residenceRepo.ListAsync();
            ViewBag.Clients = await _clientRepo.ListAsync();
            return View(reservation);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var options = new QueryOptions<Reservation>
            {
                Where = r => r.ReservationId == id
            };
            options.Includes.Add(r => r.Residence);
            options.Includes.Add(r => r.Client);

            var reservation = await _resRepo.GetAsync(options);
            if (reservation == null) return NotFound();

            return View(reservation!);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _resRepo.GetAsync(id);
            if (reservation == null) return NotFound();

            await _resRepo.DeleteAsync(reservation);
            await _resRepo.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var options = new QueryOptions<Reservation>
            {
                Where = r => r.ReservationId == id
            };
            options.Includes.Add(r => r.Residence);
            options.Includes.Add(r => r.Client);

            var reservation = await _resRepo.GetAsync(options);
            if (reservation == null) return NotFound();

            return View(reservation!); 
        }
    }
}
