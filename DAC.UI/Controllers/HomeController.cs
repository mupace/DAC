using DAC.DB.Models;
using DAC.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DAC.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DACDBContext _dacDbContext;

        public HomeController(ILogger<HomeController> logger, DACDBContext dacDbContext)
        {
            _logger = logger;
            _dacDbContext = dacDbContext;
        }

        public IActionResult Index()
        {
            _logger.LogCritical("Index ping!");
            return View();
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