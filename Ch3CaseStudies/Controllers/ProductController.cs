using Ch3CaseStudies.Models.DataLayer;
using Ch3CaseStudies.Models.DomainModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ch3CaseStudies.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        //public SportsProContext Context { get; set; }
        private Repository<Product> Products { get; set; }

        public ProductController(SportsProContext ctx)
        {
            //Context = ctx;
            Products = new Repository<Product>(ctx);
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            ViewBag.Action = "Delete Product";
            var product = Products.Get(id);
            return View(product);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Product product)
        {
            Products.Delete(product);
            Products.Save();
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
            var product = Products.Get(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductId == 0)
                {
                    Products.Insert(product);
                    TempData["Message"] = $"{product.Name} Added to Products.";
                }
                else
                {
                    Products.Update(product);
                    TempData["Message"] = $"{product.Name} Updated.";
                }
                Products.Save();
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
            //var products = Context.Products.OrderBy(p => p.Name).ToList();
            var products = Products.List(new QueryOptions<Product> { OrderBy = p => p.Name });
            ViewData["ProductTempMessage"] = TempData["Message"];
            return View(products);
        }

    }
}
