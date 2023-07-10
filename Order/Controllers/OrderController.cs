using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Order.Models;

namespace Order.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext db;
        public OrderController(ApplicationDbContext context)
        {
            db = context;
        }

        // GET: OrderController
        public IActionResult Index()
        {
            return View();
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        public IActionResult Create()
        {
            var providerFilters = new SelectList(db.Providers, "Id", "Name");
            ViewBag.Providers = providerFilters;
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(Orders order)
        {
            var providerFilters = new SelectList(db.Providers, "Id", "Name");
            ViewBag.Providers = providerFilters;

            try
            {
                if (db.Orders.Any(o => o.Number == order.Number && o.ProviderId == order.ProviderId))
                {
                    ModelState.AddModelError("", "Заказ с указанным номером и поставщиком уже существует.");
                    return View(order);
                }
                else
                {
                    db.Orders.Add(order);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception)
            {
                return View(order);
            }
                
           
           
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
