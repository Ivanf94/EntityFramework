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

        public IActionResult NewInvoice()
        {
            Invoice inv = new Invoice();
            inv.DateOfIssue = DateTime.Now;

            return View(inv);
        }

        [HttpPost]
        public IActionResult NewInvoice(Invoice invoice)
        {
            var new_invoice_number = _repo.CreateNewInvoice(invoice);

            if (new_invoice_number == null)
            {
                ViewBag.Error = "Nešto nije dobro, vjerojatno veza s bazom podataka!";
                return View(invoice);
            }

            return RedirectToAction("NewInvoiceItem", new { id = new_invoice_number });
        }

        public IActionResult NewInvoiceItem(int id)
        {
            ViewBag.InvoiceNumber = id;
            InvoiceItem items = new InvoiceItem();
            return View(items);
        }

        [HttpPost]
        public IActionResult NewInvoiceItem(InvoiceItem new_item)
        {
            int invoice_nr = int.Parse(Request.Form["InvoiceNumber"]);
            int? new_item_id = (int?)_repo.CreateNewInvoiceItem(new_item, invoice_nr);

            if(new_item_id == null)
            {
                ViewBag.Error = "Nešto je prošlo krivo prilikom spremanja.";
                ViewBag.InvoiceNumber = invoice_nr;
                return View(new_item);
            }

            return RedirectToAction("NewInvoiceItem", new { id = invoice_nr });
        }

        public IActionResult DeleteInvoice(int id)
        {
            Invoice inv = _repo.GetInvoiceByNumber(id);
            return View(inv);
        }

        [HttpPost]
        public IActionResult DeleteInvoice(int id, IFormFileCollection collection)
        {
            _repo.DeleteInvoice(id);
            return RedirectToAction("Index");
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
