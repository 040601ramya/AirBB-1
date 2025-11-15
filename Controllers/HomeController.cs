using AirBB.Models;
using AirBB.Models.ViewModels;
using AirBB.Models.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AirBB.Controllers
{
    public class HomeController : Controller
    {
        private readonly AirBnbContext _context;
        private readonly ISessionHelper _session;

        public HomeController(AirBnbContext context, ISessionHelper session)
        {
            _context = context;
            _session = session;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var savedFilter = _session.Get<HomeViewModel>("HomeFilter") ?? new HomeViewModel();
            var vm = BuildViewModel(savedFilter);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Filter(HomeViewModel form)
        {
            _session.Set("HomeFilter", form);
            var vm = BuildViewModel(form);
            return View("Index", vm);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var res = _context.Residences.Include(r => r.Location).FirstOrDefault(r => r.ResidenceId == id);
            if (res == null) return NotFound();

            var reservations = _context.Reservations
                .Where(r => r.ResidenceId == id)
                .Select(r => new { r.ReservationStartDate, r.ReservationEndDate })
                .ToList();

            var blocked = new HashSet<DateTime>();
            foreach (var r in reservations)
            {
                var d = r.ReservationStartDate.Date;
                while (d < r.ReservationEndDate.Date)
                {
                    blocked.Add(d);
                    d = d.AddDays(1);
                }
            }

            var start = DateTime.Today;
            var end = start.AddDays(90);
            var allDays = Enumerable.Range(0, (end - start).Days + 1).Select(i => start.AddDays(i)).ToList();

            var dvm = new DetailViewModel
            {
                Residence = res,
                StartDateOptions = allDays.Where(d => !blocked.Contains(d)).ToList(),
                EndDateOptions = allDays
            };

            return View(dvm);
        }

        public IActionResult Privacy() => View();
        public IActionResult Cancellation() => View();

        private HomeViewModel BuildViewModel(HomeViewModel f)
        {
            var vm = new HomeViewModel
            {
                SelectedLocationId = f.SelectedLocationId,
                StartDate = f.StartDate,
                EndDate = f.EndDate,
                GuestCount = f.GuestCount > 0 ? f.GuestCount : 1,
                Locations = _context.Locations.OrderBy(l => l.Name).ToList()
            };

            var q = _context.Residences.Include(r => r.Location).AsQueryable();

            if (vm.SelectedLocationId > 0) q = q.Where(r => r.LocationId == vm.SelectedLocationId);
            if (vm.GuestCount > 1) q = q.Where(r => r.GuestNumber >= vm.GuestCount);

            if (vm.StartDate.HasValue && vm.EndDate.HasValue)
            {
                var s = vm.StartDate.Value.Date;
                var e = vm.EndDate.Value.Date;

                var blockedIds = _context.Reservations
                    .Where(r => r.ReservationStartDate < e && r.ReservationEndDate > s)
                    .Select(r => r.ResidenceId)
                    .Distinct();

                q = q.Where(r => !blockedIds.Contains(r.ResidenceId));
            }

            vm.Residences = q.OrderBy(r => r.Location!.Name).ThenBy(r => r.Name).ToList();
            return vm;
        }

        public IActionResult Support()
        {
            ViewData["Title"] = "Support";
            return Content("Area: Public, Controller: Home, Action: Support");
        }

        public IActionResult CancellationPolicy()
        {
            ViewData["Title"] = "Cancellation Policy";
            return Content("Area: Public, Controller: Home, Action: CancellationPolicy");
        }

        public IActionResult Terms()
        {
            ViewData["Title"] = "Terms & Condition";
            return Content("Area: Public, Controller: Home, Action: Terms");
        }

        public IActionResult CookiePolicies()
        {
            ViewData["Title"] = "Cookie Policies";
            return Content("Area: Public, Controller: Home, Action: CookiePolicies");
        }
    }
}
