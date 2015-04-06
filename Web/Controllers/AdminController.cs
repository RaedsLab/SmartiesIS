using Data;
using Domain.Entities;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Web.Controllers
{
    public class AdminController : Controller
    {
        private Context db = new Context();

        IAdminService AdminService = null;

        public AdminController()
        {
            AdminService = new AdminService();
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View(db.Admin.ToList());
        }

        public ActionResult VendorsIndex()
        {
            return View(db.Admin.ToList());
        }
        public ActionResult LogOff()
        {
            if (Session["Vendor"] != null)
            { Session["Vendor"] = null; }
            if (Session["Admin"] != null)
            { Session["Admin"] = null; }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            Admin a = AdminService.LogAdmin(admin.Email, admin.Password);
            if (a != null)
            {
                Session["Admin"] = a;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult VendorLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult VendorLogin(Admin admin)
        {
            Admin a = AdminService.LogVendor(admin.Email, admin.Password);
            if (a != null)
            {
                Session["Vendor"] = a;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult VendorPanel()
        {
            if (Session["Vendor"] != null)
            {
                Admin vendor = Session["Vendor"] as Admin;
                ViewBag.vendor += vendor.Name;
                return View();
            }
            return RedirectToAction("VendorLogin");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VendorPanel([Bind(Include = "Id,Name,FamilyName,Email,Password,Tag,Privacy,CaddyId")] Client client)
        {
            if (ModelState.IsValid)
            {
                client.Password = "0000";
                client.Privacy = "1";
                db.Client.Add(client);
                db.SaveChanges();
                return View();
            }

            return View();
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}