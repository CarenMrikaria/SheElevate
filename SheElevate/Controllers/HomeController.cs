using Microsoft.AspNetCore.Mvc;
using SheElevate.Models;
using System.Diagnostics;

namespace SheElevate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            var aboutModel = new AboutModel
            {
                Title = "Welcome to SheElevate",
                Subtitle = "Empowering Women to Reach New Heights",
                Description = "SheElevate is a transformative platform dedicated to empowering women across various walks of life to achieve their personal and professional goals. By offering mentorship programs, career guidance, and community support, SheElevate helps women develop the skills, confidence, and networks they need to succeed.",
                Vision = "Our vision is to inspire, elevate, and support women worldwide, creating a future where every woman has the tools and opportunities to achieve her fullest potential.",
                CallToAction = "Join SheElevate today and be part of a global movement dedicated to creating lasting impact for women everywhere."
            };

            return View(aboutModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
