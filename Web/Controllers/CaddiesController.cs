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
    public class CaddiesController : Controller
    {
        private Context db = new Context();

        // GET: Caddies
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
            var caddy = db.Caddy.Include(c => c.Client);
            return View(caddy.ToList());
        }

        // GET: Caddies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caddy caddy = db.Caddy.Find(id);
            if (caddy == null)
            {
                return HttpNotFound();
            }
            return View(caddy);
        }

        // GET: Caddies/Create
        public ActionResult Create()
        {
            Caddy caddy = new Caddy();
            caddy.xPos = 0;
            caddy.yPos = 0;
            caddy.zPos = 0;
            caddy.ClientId = null;
            caddy.State = "Free";
            Log log = new Log();
            log.Trigger = "Caddy";
            log.Message = "New Caddy added";
            log.Time = DateTime.Now;
            log.Cide = "4";
            db.Caddy.Add(caddy);
            db.Log.Add(log);
            db.SaveChanges();

            ViewBag.ClientId = new SelectList(db.Client, "Id", "Name");
            return RedirectToAction("Index");
        }

        // POST: Caddies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,xPos,yPos,zPos,State,ClientId")] Caddy caddy)
        {
            if (ModelState.IsValid)
            {
                caddy.xPos = 0;
                caddy.yPos = 0;
                caddy.zPos = 0;
                caddy.ClientId = null;
                caddy.State = "Free";
                Log log = new Log();
                log.Trigger = "Caddy";
                log.Message = "New Caddy added";
                log.Time =  DateTime.Now;
                log.Cide = "4";
                db.Caddy.Add(caddy);
                db.Log.Add(log);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Client, "Id", "Name", caddy.ClientId);
            return View(caddy);
        }

        // GET: Caddies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caddy caddy = db.Caddy.Find(id);
            if (caddy == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Client, "Id", "Name", caddy.ClientId);
            return View(caddy);
        }

        // POST: Caddies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,xPos,yPos,zPos,State,ClientId")] Caddy caddy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caddy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Client, "Id", "Name", caddy.ClientId);
            return View(caddy);
        }

        // GET: Caddies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caddy caddy = db.Caddy.Find(id);
            if (caddy == null)
            {
                return HttpNotFound();
            }
            return View(caddy);
        }

        // POST: Caddies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Caddy caddy = db.Caddy.Find(id);
            db.Caddy.Remove(caddy);
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
