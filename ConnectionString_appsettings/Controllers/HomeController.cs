using System.Diagnostics;
using ConnectionString_appsettings.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ConnectionString_appsettings.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Invoices")))
                {
                    connection.Open();
                    ViewBag.Message = "Veza uspješno otvorena";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Greška pri povezivanju: " + ex.Message;
            }
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
