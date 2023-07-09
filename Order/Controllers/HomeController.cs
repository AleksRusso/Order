using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Order.Models;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Order.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext db;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            db = context;
        }

        public IActionResult Index()
        {
            var order = db.Orders.Include(o => o.Provider).Where(o => o.Date >= DateTime.Now.AddMonths(-1) && o.Date <= DateTime.Now).ToList();
            var numberFilters = db.Orders.Select(o => o.Number).Distinct().ToList();
            var providerFilters = db.Providers.Select(p => p.Name).Distinct().ToList();

           // var order = db.Orders.Include(o=>o.Provider).ToList();

            OrderModel model = new OrderModel();
            model.Orders = order;
            model.NumberFilters = numberFilters;
            model.ProviderFilters = providerFilters;

            return View(model);
        }

        [HttpPost]
        public IActionResult Filter(int[] number, DateTime? fromDate, DateTime? toDate, string[] providers)
        {
            var filteredOrders = db.Orders.Include(o => o.Provider).AsQueryable();

            if (number.Length > 0)
            {
                filteredOrders = filteredOrders.Where(o=> number.Contains(o.Number));
            }

            if (fromDate.HasValue && toDate.HasValue)
            {
                filteredOrders = filteredOrders.Where(o => o.Date >= fromDate.Value && o.Date <= toDate.Value);
            }

            if (providers != null && providers.Length > 0)
            {
                filteredOrders = filteredOrders.Where(o => providers.Contains(o.Provider.Name));
            }
            var numberFilters = db.Orders.Select(o => o.Number).Distinct().ToList();
            var providerFilters = db.Providers.Select(p => p.Name).Distinct().ToList();

            var orders = filteredOrders.ToList();
            OrderModel model = new OrderModel();
            model.Orders = orders;
            model.NumberFilters = numberFilters;
            model.ProviderFilters = providerFilters;

            return View("Index", model);
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