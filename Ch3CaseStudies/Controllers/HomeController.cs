//using Ch3CaseStudies.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ch3CaseStudies.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("/about")]
        public IActionResult About()
        {
            return View();
        }
    }
}
