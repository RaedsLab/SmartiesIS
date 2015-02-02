using Domain.Entities;
using Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        IProductService ProductService = null;
        public ProductController(IProductService ProductService)
        {
            this.ProductService = ProductService;
        }
        public ProductController()
        {
            ProductService = new ProductService();
        }

        // GET: Product
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

            var Products = ProductService.GetAllProducts();
            return View(Products);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var prod = ProductService.GetProductById(id);
            return View(prod);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product Prod, HttpPostedFileBase file)
        {
            Product p = Prod;

            if (file.ContentLength == 0)
            {
                return View(p);
            }

            try
            {
                p.Image = file.FileName;
                ProductService.AddNewProduct(p);
                var path = Path.Combine(Server.MapPath("~/Content/Uploads/Products"), p.Image);
                file.SaveAs(path);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(p);
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            Session["ProdEditId"] = id;
            Product p = ProductService.GetProductById(id);
            return View(p);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product Prod, HttpPostedFileBase file)
        {
            Product p = Prod;
            p.Id = id;

            if (file != null)
            {
                if (file.ContentLength != 0)
                {
                    p.Image = file.FileName;
                    var path = Path.Combine(Server.MapPath("~/Content/Uploads/Products"), p.Image);
                    file.SaveAs(path);
                }
            }
            ProductService.EditProd(p);
            return RedirectToAction("Index");

        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            ProductService.DeleteProd(id);

            return RedirectToAction("Index");
        }

    }
}
