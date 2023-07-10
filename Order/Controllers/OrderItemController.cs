﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order.Models;

namespace Order.Controllers
{
    public class OrderItemController : Controller
    {
        // GET: OrderItemController
        private readonly ApplicationDbContext db;
        public OrderItemController(ApplicationDbContext context)
        {
            db = context;
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: OrderItemController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderItemController/Create
        [HttpGet]
        public IActionResult CreateOrderItem(int orderId)
        {
            var order = db.Orders.Find(orderId);
            if (order == null)
            {
                return NotFound();
            }
            var item = new OrderItem();
            item.OrderId = orderId;
            return View(item);
        }
        [HttpPost]
        public IActionResult CreateOrderItem(OrderItem orderItem)
        {
            if (db.Orders.Any(o => o.Id == orderItem.OrderId))
            {
                if (db.OrderItems.Any(oi => oi.OrderId == orderItem.OrderId && oi.Name == orderItem.Name))
                {
                    ModelState.AddModelError("", "Name не может быть равен Number заказа.");
                }

                var order = new OrderItem
                {
                    Name = orderItem.Name,
                    Quantity = orderItem.Quantity,
                    Unit = orderItem.Unit,
                    OrderId = orderItem.OrderId
                };
                db.OrderItems.Add(orderItem);
                db.SaveChanges();
                return RedirectToAction("Show", new { id = orderItem.OrderId });
            }
            else
            {
                return NotFound();
            }

        }



        public IActionResult Show(int id)
        {
            var order = db.OrderItems.Where(o => o.OrderId == id).Include(o => o.Order).ToList();
            return View(order);

        }



        // GET: OrderItemController/Edit/5
        public IActionResult Edit(int id)
        {
            var order = db.OrderItems.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: OrderItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                db.OrderItems.Update(orderItem);
                db.SaveChanges();
                return RedirectToAction("Show");
            }
            return View(orderItem);
        }

        // GET: OrderItemController/Delete/5
        public IActionResult Delete(int id)
        {
            var orderItem = db.OrderItems.Find(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            db.OrderItems.Remove(orderItem);
            db.SaveChanges();
            return RedirectToAction("Show");
        }

    }
}
