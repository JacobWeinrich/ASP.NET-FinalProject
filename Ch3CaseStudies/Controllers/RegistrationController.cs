using Ch3CaseStudies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ch3CaseStudies.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly SportsProContext _context;
        public RegistrationController(SportsProContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            RegistrationViewModel vm = new RegistrationViewModel();
            ViewData["message"] = TempData["message"];
            vm.Customers = _context.Customers.ToList();
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {

            var customer = await _context.Customers.Include(c => c.Products).FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
            {
                TempData["message"] = $"Select a valid customer!";
                return RedirectToAction("Index");
            }

            RegistrationViewModel vm = new RegistrationViewModel();
            vm.SelectedCustomer = customer;
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel vm)
        {
            var customer = await _context.Customers.Where(c => c.CustomerId == vm.SelectedCustomerId).Include(c => c.Products).FirstOrDefaultAsync();
            if (customer != null)
            {
            customer.Products.Add(new Product() { Name = "hello", Price = 5, ProductCode = "1234", ReleaseDate = DateTime.Now});
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new {id = vm.SelectedCustomerId});
        }

        [HttpPost]
        public IActionResult SelectCustomer(RegistrationViewModel vm)
        {

            vm.Customers = _context.Customers.ToList();
            if (ModelState.IsValid)
            {
                return RedirectToAction("Details", new {id = vm.SelectedCustomerId});
            }
            return View("Index", vm);
        }
    }
}
