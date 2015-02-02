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
    public class AdsController : Controller
    {
        private Context db = new Context();

        // GET: Ads
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

            var ad = db.Ad.Include(a => a.Product);
            return View(ad.ToList());
        }

        // GET: Ads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ad ad = db.Ad.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        // GET: Ads/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name");
            return View();
        }

        // POST: Ads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StartDate,EndDate,Etat,ProductId")] Ad ad)
        {
            if (ModelState.IsValid)
            {
                db.Ad.Add(ad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name", ad.ProductId);
            return View(ad);
        }

        // GET: Ads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ad ad = db.Ad.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name", ad.ProductId);
            return View(ad);
        }

        // POST: Ads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartDate,EndDate,Etat,ProductId")] Ad ad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name", ad.ProductId);
            return View(ad);
        }

        // GET: Ads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ad ad = db.Ad.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        // POST: Ads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ad ad = db.Ad.Find(id);
            db.Ad.Remove(ad);
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
