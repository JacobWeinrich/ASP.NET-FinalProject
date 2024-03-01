using Ch3CaseStudies.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ch3CaseStudies.Controllers
{
    public class ProductController : Controller
    {
        public SportsProContext Context { get; set; }

        public ProductController(SportsProContext ctx)
        {
            Context = ctx;
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            ViewBag.Action = "Delete Product";
            var product = Context.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Product product)
        {
            Context.Products.Remove(product);
            Context.SaveChanges();
            TempData["Message"] = $"{product.Name} Removed from Products";
            return RedirectToAction("Index", "Products");
        }

        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Action = "Add Product";

            Product product = new Product();
            product.ReleaseDate = DateTime.Now.Date;

            return View("Edit", product);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            ViewBag.Action = "Edit Product";
            var product = Context.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductId == 0)
                {
                    Context.Products.Add(product);
                    TempData["Message"] = $"{product.Name} Added to Products.";
                }
                else
                {
                    Context.Products.Update(product);
                    TempData["Message"] = $"{product.Name} Updated.";
                }
                Context.SaveChanges();
                return RedirectToAction("Index", "Products");
            }
            else
            {
                ViewBag.Action = (product.ProductId == 0) ? "Add" : "Edit";
                return View(product);
            }
        }

        [HttpGet]
        public RedirectToActionResult Index()
        {
            return RedirectToAction("List", "Product");

        }
        [HttpGet]
        [Route("/products")]
        public ViewResult List()
        {
            var products = Context.Products.OrderBy(p => p.Name).ToList();
            ViewData["ProductTempMessage"] = TempData["Message"];
            return View(products);
        }

    }
}
