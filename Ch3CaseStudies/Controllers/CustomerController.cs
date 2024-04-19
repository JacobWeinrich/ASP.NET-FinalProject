using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Ch3CaseStudies.Models;
using Ch3CaseStudies.Models.DataLayer;
using Ch3CaseStudies.Models.DomainModels;


namespace Ch3CaseStudies.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomerController : Controller
    {
        private SportsProContext Context { get; set; }

        private Repository<Customer> Customers { get; set; }
        private Repository<Country> Countries { get; set; }

        public CustomerController(SportsProContext ctx)
        {
            Context = ctx;
            Customers = new Repository<Customer>(ctx);
            Countries = new Repository<Country>(ctx);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.Action = "Delete Customer";
            //var customer = Context.Customers.Find(id);
            var customer = Customers.Get(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            //Context.Customers.Remove(customer);
            //Context.SaveChanges();
            Customers.Delete(customer);
            Customers.Save();
            return RedirectToAction("Index", "Customers");
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add Customer";
            Customer customer = new Customer();
            //ViewBag.countries = Context.Countries.OrderBy(c => c.CountryId).ToList();
            ViewBag.countries = Countries.List(new QueryOptions<Country> { OrderBy = c => c.CountryId });
            return View("Edit", customer);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit Customer";
            //var customer = Context.Customers.Find(id);
            //ViewBag.countries = Context.Countries.OrderBy(c => c.CountryId).ToList();
            var customer = Customers.Get(id);
            ViewBag.countries = Countries.List(new QueryOptions<Country> { OrderBy = c => c.CountryId });
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (!string.IsNullOrEmpty(customer.Email))
            {
                string msg = "";
                msg = Check.CheckEmail(Context, customer);

                if (!string.IsNullOrEmpty(msg))
                {
                    ModelState.AddModelError(nameof(customer.Email), msg);
                }
            }


            if (ModelState.IsValid)
            {
                if (customer.CustomerId == 0)
                {
                    Customers.Insert(customer);
                }
                else
                {
                    Customers.Update(customer);
                }
                Customers.Save();
                return RedirectToAction("Index", "Customers");
            }
            else
            {
                //ViewBag.countries = Context.Countries.OrderBy(c => c.CountryId).ToList();
                ViewBag.countries = Countries.List(new QueryOptions<Country> { OrderBy = c => c.CountryId });
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
            //var customers = Context.Customers.OrderBy(t => t.LastName).ToList();
            var customerList = Customers.List(new QueryOptions<Customer> { OrderBy = t => t.LastName! });
            return View(customerList);
        }
    }
}
