using System.Diagnostics;
using ConnectionString_custom_repo.Models;
using Microsoft.AspNetCore.Mvc;
using ConnectionString_custom_repo.Repository;

namespace ConnectionString_custom_repo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        HomeRepository _repo;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _repo = new HomeRepository(configuration);
            _logger = logger;
        }

        public IActionResult Index()
        {
            bool check = _repo.CheckExistingConnection();
            ViewBag.Message = (check == true) ? "Veza uspostavljena" : "Veza nije uspostavljena";
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
