using Ch3CaseStudies.Models;
using Ch3CaseStudies.Models.DataLayer;
using Ch3CaseStudies.Models.DomainModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ch3CaseStudies.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IncidentController : Controller
    {

        //public SportsProContext Context { get; set; }
        
        private Repository<Customer> Customers { get; set; }
        private Repository<Incident> Incidents { get; set; }
        private Repository<Product> Products { get; set; }
        private Repository<Technician> Technicians { get; set; }


        public IncidentController(SportsProContext ctx)
        {
            //Context = ctx;
            Customers = new Repository<Customer>(ctx);
            Incidents = new Repository<Incident>(ctx);
            Products = new Repository<Product>(ctx);
            Technicians = new Repository<Technician>(ctx);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.Action = "Delete Incident";
            //var incident = Context.Incidents.Find(id);
            var incident = Incidents.Get(id);
            if (incident != null)
            {
                //incident.Technician = Context.Technicians.Find(incident.TechnicianId);
                //incident.Customer = Context.Customers.Find(incident.CustomerId);
                //incident.Product = Context.Products.Find(incident.ProductId);
                incident.Technician = Technicians.Get(incident.TechnicianId ?? 0)!;
                incident.Customer = Customers.Get(incident.CustomerId ?? 0)!;
                incident.Product = Products.Get(incident.ProductId ?? 0)!;
            }
            return View(incident);
        }

        [HttpPost]
        public IActionResult Delete(Incident incident)
        {
            Incidents.Delete(incident);
            Incidents.Save();
            return RedirectToAction("Index", "Incident");

        }

        [HttpGet]
        public IActionResult Add()
        {
            //ViewBag.Action = "Add Incident";
            //ViewBag.Technicians = Context.Technicians.OrderBy(t => t.Name).ToList();
            //ViewBag.Customers = Context.Customers.OrderBy(c => c.LastName).ToList();
            //ViewBag.Products = Context.Products.OrderBy(p => p.Name).ToList();
            //Incident incident = new Incident();
            //incident.DateOpened = DateTime.Now.Date;
            IncidentEditViewModel vm = new IncidentEditViewModel();
            vm.Action = "Add Incident";
            //vm.Technicians = Context.Technicians.OrderBy(t => t.Name).ToList();
            //vm.Customers = Context.Customers.OrderBy(c => c.LastName).ToList();
            //vm.Products = Context.Products.OrderBy(p => p.Name).ToList();
            vm.Technicians = (List<Technician>)Technicians.List(new QueryOptions<Technician> { OrderBy = t => t.Name });
            vm.Customers = (List<Customer>)Customers.List(new QueryOptions<Customer> { OrderBy = c => c.LastName });
            vm.Products = (List<Product>)Products.List(new QueryOptions<Product> { OrderBy = p => p.Name });
            vm.Incident.DateOpened = DateTime.Now.Date;
            return View("Edit", vm);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //ViewBag.Action = "Edit Incident";
            //ViewBag.Technicians = Context.Technicians.OrderBy(t => t.Name).ToList();
            //ViewBag.Customers = Context.Customers.OrderBy(c => c.LastName).ToList();
            //ViewBag.Products = Context.Products.OrderBy(p => p.Name).ToList();
            //var incident = Context.Incidents.Find(id);
            IncidentEditViewModel vm = new IncidentEditViewModel();
            vm.Action = "Edit Incident";
            Incident? incident = Incidents.Get(id);
            vm.Incident = incident != null ? incident : new Incident();
            //vm.Technicians = Technicians.OrderBy(t => t.Name).ToList();
            //vm.Customers = Context.Customers.OrderBy(c => c.LastName).ToList();
            //vm.Products = Context.Products.OrderBy(p => p.Name).ToList();
            vm.Technicians = (List<Technician>)Technicians.List(new QueryOptions<Technician> { OrderBy = t => t.Name });
            vm.Customers = (List<Customer>)Customers.List(new QueryOptions<Customer> { OrderBy = c => c.LastName });
            vm.Products = (List<Product>)Products.List(new QueryOptions<Product> { OrderBy = p => p.Name });
            vm.Incident.DateOpened = DateTime.Now.Date;
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(Incident incident)
        {
            if (ModelState.IsValid)
            {
                if (incident.IncidentId == 0)
                {
                    Incidents.Insert(incident);
                }
                else
                {
                    Incidents.Update(incident);
                }
                Incidents.Save();
                return RedirectToAction("Index", "Incident");
            }
            else
            {
                IncidentEditViewModel vm = new IncidentEditViewModel();
                vm.Technicians = (List<Technician>)Technicians.List(new QueryOptions<Technician> { OrderBy = t => t.Name });
                vm.Customers = (List<Customer>)Customers.List(new QueryOptions<Customer> { OrderBy = c => c.LastName });
                vm.Products = (List<Product>)Products.List(new QueryOptions<Product> { OrderBy = p => p.Name });
                vm.Incident = incident;
                vm.Action = (incident.IncidentId == 0) ? "Add" : "Edit";
                //ViewBag.Technicians = Context.Technicians.OrderBy(t => t.Name).ToList();
                //ViewBag.Customers = Context.Customers.OrderBy(c => c.LastName).ToList();
                //ViewBag.Products = Context.Products.OrderBy(p => p.Name).ToList();
                //ViewBag.Action = (incident.IncidentId == 0) ? "Add" : "Edit";
                return View(vm);
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("List", "Incident");
        }

        [HttpGet]
        [Route("/incidents/{sort?}")]

        public IActionResult List(string sort)
        {
            if (sort == "unassigned")
            {
                //var incidents = Context.Incidents.Include(c => c.Customer).Include(t => t.Technician).Include(p => p.Product).OrderBy(t => t.IncidentId * -1).Where(t => t.TechnicianId == null).ToList();
                var incidentsList = Incidents.List(new QueryOptions<Incident> {OrderBy = t => t.IncidentId * -1, Where = t => t.TechnicianId == null, Includes = "Customer, Technician, Product" });
                return View(incidentsList);
            }
            else if (sort == "open")
            {
                //var incidents = Context.Incidents.Include(c => c.Customer).Include(t => t.Technician).Include(p => p.Product).OrderBy(t => t.IncidentId * -1).Where(c => c.DateClosed == null).ToList();
                var incidentsList = Incidents.List(new QueryOptions<Incident> { OrderBy = t => t.IncidentId * -1, Where = t => t.DateClosed == null, Includes = "Customer, Technician, Product" });
                return View(incidentsList);
            }else if(sort == "all")
            {
                //var incidentsList = Context.Incidents.Include(c => c.Customer).Include(t => t.Technician).Include(p => p.Product).OrderBy(t => t.IncidentId * -1).ToList();
                var incidentsList = Incidents.List(new QueryOptions<Incident> { OrderBy = t => t.IncidentId * -1, Includes = "Customer, Technician, Product" });
                return View(incidentsList);
            }
            else
            {
              return RedirectToAction("List", "Incident", new { sort = "all" });
            }
        }
    }
}
