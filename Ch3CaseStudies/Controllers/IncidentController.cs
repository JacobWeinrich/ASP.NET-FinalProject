﻿using Ch3CaseStudies.Models;
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
            //ViewBag.Action = "Add Incident";
            //ViewBag.Technicians = Context.Technicians.OrderBy(t => t.Name).ToList();
            //ViewBag.Customers = Context.Customers.OrderBy(c => c.LastName).ToList();
            //ViewBag.Products = Context.Products.OrderBy(p => p.Name).ToList();
            //Incident incident = new Incident();
            //incident.DateOpened = DateTime.Now.Date;
            IncidentEditViewModel vm = new IncidentEditViewModel();
            vm.Action = "Add Incident";
            vm.Technicians = Context.Technicians.OrderBy(t => t.Name).ToList();
            vm.Customers = Context.Customers.OrderBy(c => c.LastName).ToList();
            vm.Products = Context.Products.OrderBy(p => p.Name).ToList();
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
            Incident? incident = Context.Incidents.Find(id);
            vm.Incident = incident != null ? incident : new Incident();
            vm.Technicians = Context.Technicians.OrderBy(t => t.Name).ToList();
            vm.Customers = Context.Customers.OrderBy(c => c.LastName).ToList();
            vm.Products = Context.Products.OrderBy(p => p.Name).ToList();
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
                IncidentEditViewModel vm = new IncidentEditViewModel();
                vm.Technicians = Context.Technicians.OrderBy(t => t.Name).ToList();
                vm.Customers = Context.Customers.OrderBy(c => c.LastName).ToList();
                vm.Products = Context.Products.OrderBy(p => p.Name).ToList();
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
        [Route("/incidents")]
        public IActionResult List()
        {
            var incidents = Context.Incidents.Include(c => c.Customer).Include(t => t.Technician).Include(p => p.Product).OrderBy(t => t.IncidentId * -1).ToList();
            return View(incidents);
        }
    }
}
