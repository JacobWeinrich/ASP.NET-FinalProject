using Ch3CaseStudies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ch3CaseStudies.Controllers
{
    public class TechIncidentController : Controller
    {
        private const string TECH_KEY = "techId";
        private SportsProContext context { get; set; }
        public TechIncidentController(SportsProContext ctx) => context = ctx;

        public IActionResult Index()
        {
            ViewData["message"] = TempData["message"];
            ViewBag.Technicians = context.Technicians.OrderBy(c => c.Name).ToList();
            var technician = new Technician();
            int? techID = HttpContext.Session.GetInt32(TECH_KEY);
            if (techID.HasValue)
            {
                technician = context.Technicians.Find(techID);
            }
            return View(technician);
        }

        [HttpPost]
        public IActionResult List(Technician technician)
        {
            if (technician.TechnicianId <= 0)
            {
                TempData["message"] = "Please select a technician from the list.";
                return RedirectToAction("Index");
            } else
            {
                HttpContext.Session.SetInt32(TECH_KEY, technician.TechnicianId);
                return RedirectToAction("List", new {id = technician.TechnicianId});
            }
        }

        [HttpGet]
        public IActionResult List(int id)
        {
            int? techID = HttpContext.Session.GetInt32(TECH_KEY);
            if (!techID.HasValue)
            {
                TempData["message"] = "Please select a technician from the list.";
                return RedirectToAction("Index");
            }
            TechIncidentViewModel vm = new TechIncidentViewModel();
            vm.Technician = context.Technicians.Find(id) ?? new Technician();
            vm.Incidents = context.Incidents.Where(i => i.TechnicianId == id).Where(d => d.DateClosed == null).Include(i => i.Customer).Include(p => p.Product).ToList();
            return View(vm);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            int? techID = HttpContext.Session.GetInt32(TECH_KEY);
            if (!techID.HasValue)
            {
                TempData["message"] = "Please select a technician from the list.";
                return RedirectToAction("Index");
            }
            var technician = context.Technicians.Find(techID);
            if (technician == null)
            {
                TempData["message"] = "Please select a technician from the list.";
                return RedirectToAction("Index");
            } else
            {
                var vm = new TechIncidentViewModel();
                vm.Technician = technician;
                vm.Incident = context.Incidents.Include(t => t.Technician).Include(c => c.Customer).Include(p => p.Product).FirstOrDefault(i => i.IncidentId == id)!;
                return View(vm);
            }
        }

        [HttpPost]
        public IActionResult Edit(TechIncidentViewModel vm)
        {
           Incident i = context.Incidents.Find(vm.Incident.IncidentId)!;
            i.Description = vm.Incident.Description;
            i.DateClosed = vm.Incident.DateClosed;

            context.Incidents.Update(i);
            context.SaveChanges();

            int? techID = HttpContext.Session.GetInt32(TECH_KEY);
            return RedirectToAction("List", new {id = techID});
        }
    }
}
