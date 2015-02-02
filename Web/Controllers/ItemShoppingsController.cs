using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data;
using Domain.Entities;

namespace Web.Controllers
{
    public class ItemShoppingsController : Controller
    {
        private Context db = new Context();

        // GET: ItemShoppings
        public ActionResult Index()
        {
            var itemShopping = db.ItemShopping.Include(i => i.Product).Include(i => i.ShoppingList);
            return View(itemShopping.ToList());
        }

        // GET: ItemShoppings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemShopping itemShopping = db.ItemShopping.Find(id);
            if (itemShopping == null)
            {
                return HttpNotFound();
            }
            return View(itemShopping);
        }

        // GET: ItemShoppings/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name");
            ViewBag.ShoppingListId = new SelectList(db.ShoppingList, "Id", "Name");
            return View();
        }

        // POST: ItemShoppings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Brand,Category,Quantity,ProductId,ShoppingListId")] ItemShopping itemShopping)
        {
            if (ModelState.IsValid)
            {
                db.ItemShopping.Add(itemShopping);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name", itemShopping.ProductId);
            ViewBag.ShoppingListId = new SelectList(db.ShoppingList, "Id", "Name", itemShopping.ShoppingListId);
            return View(itemShopping);
        }

        // GET: ItemShoppings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemShopping itemShopping = db.ItemShopping.Find(id);
            if (itemShopping == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name", itemShopping.ProductId);
            ViewBag.ShoppingListId = new SelectList(db.ShoppingList, "Id", "Name", itemShopping.ShoppingListId);
            return View(itemShopping);
        }

        // POST: ItemShoppings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Brand,Category,Quantity,ProductId,ShoppingListId")] ItemShopping itemShopping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemShopping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name", itemShopping.ProductId);
            ViewBag.ShoppingListId = new SelectList(db.ShoppingList, "Id", "Name", itemShopping.ShoppingListId);
            return View(itemShopping);
        }

        // GET: ItemShoppings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemShopping itemShopping = db.ItemShopping.Find(id);
            if (itemShopping == null)
            {
                return HttpNotFound();
            }
            return View(itemShopping);
        }

        // POST: ItemShoppings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemShopping itemShopping = db.ItemShopping.Find(id);
            db.ItemShopping.Remove(itemShopping);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
