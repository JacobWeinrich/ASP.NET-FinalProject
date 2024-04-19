using Ch3CaseStudies.Models.DataLayer;
using Ch3CaseStudies.Models.DomainModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ch3CaseStudies.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TechnicianController : Controller
    {
        //public SportsProContext Context { get; set; }
        private Repository<Technician> Technicians { get; set; }

        public TechnicianController(SportsProContext ctx)
        {
            //Context = ctx;
            Technicians = new Repository<Technician>(ctx);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.Action = "Delete Technician";
            var technician = Technicians.Get(id);
            return View(technician);
        }

        [HttpPost]
        public IActionResult Delete(Technician technician)
        {
            Technicians.Delete(technician);
            Technicians.Save();
            return RedirectToAction("Index", "Technicians");
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add Technician";

            Technician technician = new Technician();

            return View("Edit", technician);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit Technician";
            //var technician = Context.Technicians.Find(id);
            var technician = Technicians.Get(id);
            return View(technician);
        }

        [HttpPost]
        public IActionResult Edit(Technician technician)
        {
            if (ModelState.IsValid)
            {
                if (technician.TechnicianId == 0)
                {
                    //Context.Technicians.Add(technician);
                    Technicians.Insert(technician);
                }
                else
                {
                    //Context.Technicians.Update(technician);
                    Technicians.Update(technician);
                }
                //Context.SaveChanges();
                Technicians.Save();
                return RedirectToAction("Index", "Technicians");
            }
            else
            {
                ViewBag.Action = (technician.TechnicianId == 0) ? "Add" : "Edit";
                return View(technician);
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("List", "Technician");

        }


        [HttpGet]
        [Route("/technicians")]
        public IActionResult List()
        {
            //var technicians = Context.Technicians.OrderBy(t => t.Name).Where(t => t.TechnicianId > 0).ToList();
            var technicians = Technicians.List(new QueryOptions<Technician>
            {
                OrderBy = t => t.Name,
                Where = t => t.TechnicianId > 0
            });
            return View(technicians);
        }
    }
}
