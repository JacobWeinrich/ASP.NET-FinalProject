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
        public IActionResult Delete(int id)
        {
            ViewBag.Action = "Delete Product";
            var product = Context.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            Context.Products.Remove(product);
            Context.SaveChanges();
            return RedirectToAction("Index", "Products");
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add Product";

            Product product = new Product();
            product.ReleaseDate = DateTime.Now.Date;

            return View("Edit", product);
        }

        [HttpGet]
        public IActionResult Edit(int id)
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
                }
                else
                {
                    Context.Products.Update(product);
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
        public IActionResult Index()
        {
            return RedirectToAction("List", "Product");

        }
        [HttpGet]
        [Route("/products")]
        public IActionResult List()
        {
            var products = Context.Products.OrderBy(p => p.Name).ToList();
            return View(products);
        }

    }
}
