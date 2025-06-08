using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Racuni.Models;
using Racuni.Repository;

namespace Racuni.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private InvoiceRepository _repo;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _repo = new InvoiceRepository(configuration);
        }

        public IActionResult Index()
        {
            List<Invoice> invoices = _repo.GetInvoices();
            return View(invoices);
        }

        public IActionResult Details(int id)
        {
            Invoice invoice = _repo.GetInvoiceByNumber(id);
            return View(invoice);
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
