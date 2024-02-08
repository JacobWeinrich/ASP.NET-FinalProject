using Ch3CaseStudies.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ch3CaseStudies.Controllers
{
    public class CustomerController : Controller
    {
        public SportsProContext Context { get; set; }

        public CustomerController(SportsProContext ctx)
        {
            Context = ctx;
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.Action = "Delete Customer";
            var customer = Context.Customers.Find(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            Context.Customers.Remove(customer);
            Context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add Customer";
            Customer customer = new Customer(); 
            ViewBag.countries = Context.Countries.OrderBy(c => c.CountryId).ToList();
            return View("Edit", customer);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit Customer";
            var customer = Context.Customers.Find(id);
            ViewBag.countries = Context.Countries.OrderBy(c => c.CountryId).ToList();
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.CustomerId == 0)
                {
                    Context.Customers.Add(customer);
                }
                else
                {
                    Context.Customers.Update(customer);
                }
                Context.SaveChanges();
                return RedirectToAction("Index", "Customers");
            }
            else
            {
                ViewBag.countries = Context.Countries.OrderBy(c => c.CountryId).ToList();
                ViewBag.Action = (customer.CustomerId == 0) ? "Add" : "Edit";
                return View(customer);
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("List", "Customer");

        }

        [HttpGet]
        [Route("/customers")]
        public IActionResult List()
        {
            var customers = Context.Customers.OrderBy(t => t.LastName).ToList();
            return View(customers);
        }
    }
}
