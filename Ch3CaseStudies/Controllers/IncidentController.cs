using Ch3CaseStudies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ch3CaseStudies.Controllers
{
    public class IncidentController : Controller
    {

        public SportsProContext Context { get; set; }

        public IncidentController(SportsProContext ctx)
        {
            Context = ctx;
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.Action = "Delete Incident";
            var incident = Context.Incidents.Find(id);
            if (incident != null)
            {
                incident.Technician = Context.Technicians.Find(incident.TechnicianId);
                incident.Customer = Context.Customers.Find(incident.CustomerId);
                incident.Product = Context.Products.Find(incident.ProductId);
            }
            return View(incident);
        }

        [HttpPost]
        public IActionResult Delete(Incident incident)
        {
            Context.Incidents.Remove(incident);
            Context.SaveChanges();
            return RedirectToAction("Index", "Incident");
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add Incident";
            ViewBag.Technicians = Context.Technicians.OrderBy(t => t.Name).ToList();
            ViewBag.Customers = Context.Customers.OrderBy(c => c.LastName).ToList();
            ViewBag.Products = Context.Products.OrderBy(p => p.Name).ToList();
            Incident incident = new Incident();
            incident.DateOpened = DateTime.Now.Date;

            return View("Edit", incident);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit Incident";
            ViewBag.Technicians = Context.Technicians.OrderBy(t => t.Name).ToList();
            ViewBag.Customers = Context.Customers.OrderBy(c => c.LastName).ToList();
            ViewBag.Products = Context.Products.OrderBy(p => p.Name).ToList();
            var incident = Context.Incidents.Find(id);
            return View(incident);
        }

        [HttpPost]
        public IActionResult Edit(Incident incident)
        {
            if (ModelState.IsValid)
            {
                if (incident.IncidentId == 0)
                {
                    Context.Incidents.Add(incident);
                }
                else
                {
                    Context.Incidents.Update(incident);
                }
                Context.SaveChanges();
                return RedirectToAction("Index", "Incident");
            }
            else
            {
                ViewBag.Technicians = Context.Technicians.OrderBy(t => t.Name).ToList();
                ViewBag.Customers = Context.Customers.OrderBy(c => c.LastName).ToList();
                ViewBag.Products = Context.Products.OrderBy(p => p.Name).ToList();
                ViewBag.Action = (incident.IncidentId == 0) ? "Add" : "Edit";
                return View(incident);
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("List", "Incident");
        }

        [HttpGet]
        [Route("/incidents")]
        public IActionResult List()
        {
            var incidents = Context.Incidents.Include(c => c.Customer).Include(t => t.Technician).Include(p => p.Product).OrderBy(t => t.IncidentId * -1).ToList();
            return View(incidents);
        }
    }
}
