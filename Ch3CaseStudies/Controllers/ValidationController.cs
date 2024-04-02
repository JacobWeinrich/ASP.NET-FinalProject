using Ch3CaseStudies.Models.DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace Ch3CaseStudies.Controllers
{
    public class ValidationController : Controller
    {
        private SportsProContext _context;
        public ValidationController(SportsProContext context)
        {
            _context = context;
        }

        public JsonResult CheckEmail(string email)
        {
            if (_context.Customers.Any(c => c.Email == email))
            {
                return Json($"Email is already in use.");
            }
            else
            {
                return Json(true);
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
