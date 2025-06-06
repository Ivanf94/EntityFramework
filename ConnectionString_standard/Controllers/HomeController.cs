using System.Diagnostics;
using ConnectionString_standard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ConnectionString_standard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string _connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=invoices;Integrated Security=true;"+
            "TrustServercertificate=true;";
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    ViewBag.Message = "Veza s bazom podataka uspostavljena!";
                }
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Greška u vezi s bazom podataka:" + ex.Message;
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
