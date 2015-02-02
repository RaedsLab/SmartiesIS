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
    public class AylesController : Controller
    {
        private Context db = new Context();

        // GET: Ayles
        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                Admin a = Session["Admin"] as Admin;
                ViewBag.Admin += a.Name;
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

            return View(db.Ayle.ToList());
        }

        // GET: Ayles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ayle ayle = db.Ayle.Find(id);
            if (ayle == null)
            {
                return HttpNotFound();
            }
            return View(ayle);
        }

        // GET: Ayles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ayles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,xPos,yPos,zPos,ShopName,Category")] Ayle ayle)
        {
            if (ModelState.IsValid)
            {
                db.Ayle.Add(ayle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ayle);
        }

        // GET: Ayles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ayle ayle = db.Ayle.Find(id);
            if (ayle == null)
            {
                return HttpNotFound();
            }
            return View(ayle);
        }

        // POST: Ayles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,xPos,yPos,zPos,ShopName,Category")] Ayle ayle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ayle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ayle);
        }

        // GET: Ayles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ayle ayle = db.Ayle.Find(id);
            if (ayle == null)
            {
                return HttpNotFound();
            }
            return View(ayle);
        }

        // POST: Ayles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ayle ayle = db.Ayle.Find(id);
            db.Ayle.Remove(ayle);
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
