using Data;
using Domain.Entities;
using Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        IProductService ProductService = null;
        IItemShoppingService ItemService = null;
        IShoppingListService ShoppingListService = null;
        ICaddyService CaddyService = null;
        IAdService AdService = null;
        IClientService ClientService = null;

        public HomeController()
        {
            ProductService = new ProductService();
            ItemService = new ItemShoppingService();
            ShoppingListService = new ShoppingListService();
            CaddyService = new CaddyService();
            AdService = new AdService();
            ClientService = new ClientService();
        }

        public ActionResult Index()
        {
            TempData.Clear();
            if (Session["Admin"] != null)
            {
                Admin a = Session["Admin"] as Admin;
                ViewBag.Admin += a.Name;
            }
            else if (Session["Vendor"] != null)
            {
                return RedirectToAction("VendorPanel", "Admin");
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }


            var prods = ProductService.GetAllProducts();
            var items = ItemService.GetAll();

            var shoppingLists = ShoppingListService.GetAll();

            int count = 0;
            int count1 = 0;

            foreach (var i in items)
            {
                if (i.ProductId != null)
                {
                    Product p = ProductService.GetProductById((int)i.ProductId);
                    count++;
                    if (TempData.ContainsKey(p.Name))
                    {
                        TempData[p.Name] = 1 + (int)TempData[p.Name];
                    }
                    else
                    {
                        TempData.Add(p.Name, 1);
                    }
                }
            }
            TempData.Add("PieProds", count);
            //////
            count = 0;
            foreach (var i in shoppingLists)
            {
                if (i.Finished != null)
                {
                    if ((bool)i.Finished)
                    {
                        count++;
                    }
                    else
                    {
                        count1++;
                    }
                }
            }
            TempData.Add("SlFinished", count);
            TempData.Add("SlUnFinished", count1);

            var caddies = CaddyService.GetAll();

            int totalCaddies = caddies.Count();
            int freeCaddies = 0;
            foreach (Caddy caddy in caddies)
            {
                if (caddy.State == "Free")
                {
                    freeCaddies += 1;
                }
            }
            ViewBag.freeCaddies = freeCaddies;
            ViewBag.caddies = (int)(freeCaddies / totalCaddies) * 100;
            ViewBag.products = prods.Count();
            ViewBag.ads = AdService.GetAll().Count();
            ViewBag.users = ClientService.GetAll().Count();

            return View();
        }

        public ActionResult About()
        {

            //   ViewBag.Message = "Your application session: " + Session["TEST"];
            ViewBag.Message = "Your application session: ";

            if (Session["Admin"] != null)
            {
                Admin a = Session["Admin"] as Admin;
                ViewBag.Message += " Name : " + a.Name;
            }


            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult StatProduct()
        {
            var prods = ProductService.GetAllProducts();
            var items = ItemService.GetAll();

            int count = 0;
            foreach (var i in items)
            {

                if (i.ProductId != null)
                {
                    Product p = ProductService.GetProductById((int)i.ProductId);
                    count++;
                    if (ViewData.ContainsKey(p.Name))
                    {
                        ViewData[p.Name] = 1 + (int)ViewData[p.Name];
                    }
                    else
                    {
                        ViewData.Add(p.Name, 1);
                    }
                }
            }
            ViewBag.count = count;
            // ViewData.Add(stats);
            // ViewBag.stats = stats;
            return View();
        }

        public ActionResult StatSails()
        {
            var prods = ProductService.GetAllProducts();
            var items = ItemService.GetAll();


            return View();
        }
    }
}