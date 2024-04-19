using Ch3CaseStudies.Models;
using Ch3CaseStudies.Models.DomainModels;
using Ch3CaseStudies.Models.DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;

namespace Ch3CaseStudies.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RegistrationController : Controller
    {
        //private readonly SportsProContext _context;
        private Repository<Customer> Customers { get; set; }
        private Repository<Product> Products { get; set; }

        public RegistrationController(SportsProContext context)
        {
            //_context = context;
            Customers = new Repository<Customer>(context);
            Products = new Repository<Product>(context);
        }

        public IActionResult Index()
        {
            RegistrationViewModel vm = new RegistrationViewModel();
            ViewData["message"] = TempData["message"];
            //vm.Customers = _context.Customers.ToList();
            vm.Customers = (List<Customer>)Customers.List(new QueryOptions<Customer> { OrderBy = c => c.FirstName });
            return View(vm);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            ViewData["message"] = TempData["message"];
            //var customer = await _context.Customers.Include(c => c.Products).FirstOrDefaultAsync(c => c.CustomerId == id);
            var customer = Customers.Get(new QueryOptions<Customer>
            {
                Includes = "Products",
                Where = c => c.CustomerId == id
            });

            if (customer == null)
            {
                TempData["message"] = $"Select a valid customer!";
                return RedirectToAction("Index");
            }

            RegistrationViewModel vm = new RegistrationViewModel();
            vm.SelectedCustomer = customer;
            // vm.Products = (List<Product>)Products.List(new QueryOptions<Product> { OrderBy = p => p.Name, Where = p => p.ProductId != customer.Products. });

            vm.Products = Products
             .List(new QueryOptions<Product> { OrderBy = p => p.Name })
             .Where(p => !customer.Products.Any(cp => cp.ProductId == p.ProductId))
             .ToList();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Register(RegistrationViewModel vm)
        {
            //var customer = await _context.Customers.Where(c => c.CustomerId == vm.SelectedCustomerId).Include(c => c.Products).FirstOrDefaultAsync();
            var customer = Customers.Get(new QueryOptions<Customer>
            {
                Includes = "Products",
                Where = c => c.CustomerId == vm.SelectedCustomerId
            });
            if (customer != null)
            {
                if (vm.SelectedProductId != null)
                {
                    var product = Products.Get(vm.SelectedProductId!.Value);
                    if (product != null)
                    {
                        if (customer.Products.Any(p => p.ProductId == product.ProductId))
                        {
                            ModelState.AddModelError("SelectedProductId", "Product Already Added!");
                        }
                        else
                        {
                            customer.Products.Add(product);
                            Customers.Save();
                            TempData["message"] = $"Product '{product.Name}' Added To Customer";
                            return RedirectToAction("Details", new { id = vm.SelectedCustomerId });
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("SelectedProductId", "Select a Valid Product.");
                    }
                }
                else
                {
                    ModelState.AddModelError("SelectedProductId", "Select a Product.");
                }


            }


            vm.SelectedCustomer = customer;
            vm.Products = Products
            .List(new QueryOptions<Product> { OrderBy = p => p.Name })
            .Where(p => !customer.Products.Any(cp => cp.ProductId == p.ProductId))
            .ToList();
            return View("Details", vm);
        }

        [HttpPost]
        public IActionResult SelectCustomer(RegistrationViewModel vm)
        {

            //vm.Customers = _context.Customers.ToList();
            vm.Customers = (List<Customer>)Customers.List(new QueryOptions<Customer> { });
            if (ModelState.IsValid)
            {
                return RedirectToAction("Details", new { id = vm.SelectedCustomerId });
            }
            return View("Index", vm);
        }

        [HttpPost]
        public IActionResult Remove(RegistrationViewModel vm)
        {
            //var customer = await _context.Customers.Include(c => c.Products).FirstOrDefaultAsync(c => c.CustomerId == vm.SelectedCustomerId);
            var customer = Customers.Get(new QueryOptions<Customer>
            {
                Includes = "Products",
                Where = c => c.CustomerId == vm.SelectedCustomerId
            });

            if (customer != null)
            {
                var product = customer.Products.FirstOrDefault(p => p.ProductId == vm.SelectedProductId);
                if (product != null)
                {
                    customer.Products.Remove(product);
                    Customers.Save();
                    TempData["message"] = $"Product: '{product.Name}' Removed from Customer";
                }
            }
            return RedirectToAction("Details", new { id = vm.SelectedCustomerId });
        }
    }
}
