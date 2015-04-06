using Data;
using Domain.Entities;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace Mobile.Controllers
{
    public class HomeController : Controller
    {
        private Context db = new Context();

        IShoppingListService ShoppingListService = null;
        IProductService ProductService = null;
        IClientService ClientService = null;
        IItemShoppingService ItemShoppingService = null;
        public HomeController()
        {
            ProductService = new ProductService();
            ShoppingListService = new ShoppingListService();
            ClientService = new ClientService();
            ItemShoppingService = new ItemShoppingService();
        }

        public ActionResult Index()
        {

            if (Response.Cookies["User"].Value != null)
            {
                Session["User"] = ClientService.GetById(int.Parse(Response.Cookies["User"].Value));
            }
            if (Session["User"] == null)
            {
                return RedirectToAction("LoginClient", "Home");
            }

            Client Client = Session["User"] as Client;

            List<ItemShopping> initList = ItemShoppingService.GetAll().ToList();
            List<String> Cat = new List<string>();
            List<String> Brand = new List<string>();


            foreach (var item in initList)
            {
                item.ShoppingList = ShoppingListService.GetById((int)item.ShoppingListId);
                int id = (int)item.Id;
                ItemShopping sitem = ItemShoppingService.GetById(id);

                if (sitem.ShoppingList.ClientId == Client.Id)
                {
                    if (sitem.Category != null)
                    {
                        Cat.Add(sitem.Category.ToString());
                    } if (sitem.Brand != null)
                    {
                        Brand.Add(sitem.Brand.ToString());
                    }
                    if (sitem.Product != null)
                    {
                        Cat.Add(sitem.Product.Category.ToString());
                        Brand.Add(sitem.Product.Brand.ToString());
                    }

                }
            }

            var g = Cat.GroupBy(i => i);
            var k = Brand.GroupBy(i => i);
            int max = 0;
            string maxCat = "";
            string maxBrand = "";

            foreach (var grp in g)
            {
                if (grp.Count() > max)
                {
                    maxCat = grp.Key;
                }
            }
            max = 0;

            foreach (var grp in k)
            {
                if (grp.Count() > max)
                {
                    maxBrand = grp.Key;
                }
            }

            List<Product> prods = ProductService.GetAllProducts().ToList();
            int ct = 0;
            foreach (var p in prods)
            {
                if (p.Category.Contains(maxCat) || p.Brand.Contains(maxBrand))
                {
                    if (ct == 0)
                    {
                        ViewBag.p1 = p;
                    }
                    else if (ct == 1)
                    {
                        ViewBag.p2 = p;
                    }
                    else if (ct == 2)
                    {
                        ViewBag.p3 = p;
                    }
                    else if (ct >= 3)
                    {
                        break;
                    }

                    ct++;
                }
            }
            if (ct < 3)
            {
                Random rnd = new Random();
                int r = rnd.Next(prods.Count);

                if (ct == 0)
                {
                    ViewBag.p1 = prods[r];
                    ViewBag.p2 = ViewBag.p1;
                    while (ViewBag.p1 == ViewBag.p2)
                    {
                        rnd = new Random();
                        r = rnd.Next(prods.Count);
                        ViewBag.p2 = prods[r];
                    }
                    ViewBag.p3 = ViewBag.p1;
                    while (ViewBag.p3 == ViewBag.p2 || ViewBag.p3 == ViewBag.p1)
                    {
                        rnd = new Random();
                        r = rnd.Next(prods.Count);
                        ViewBag.p3 = prods[r];
                    }


                }
                else if (ct == 1)
                {
                    ViewBag.p2 = ViewBag.p1;
                    while (ViewBag.p1 == ViewBag.p2)
                    {
                        rnd = new Random();
                        r = rnd.Next(prods.Count);
                        ViewBag.p2 = prods[r];
                    }
                    ViewBag.p3 = ViewBag.p1;
                    while (ViewBag.p3 == ViewBag.p2 || ViewBag.p3 == ViewBag.p1)
                    {
                        rnd = new Random();
                        r = rnd.Next(prods.Count);
                        ViewBag.p3 = prods[r];
                    }
                }
                else
                {
                    ViewBag.p3 = ViewBag.p1;
                    while (ViewBag.p3 == ViewBag.p2 || ViewBag.p3 == ViewBag.p1)
                    {
                        rnd = new Random();
                        r = rnd.Next(prods.Count);
                        ViewBag.p3 = prods[r];
                    }
                }

            }


            return View();
        }

        public ActionResult LogOff()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LoginClient()
        {
            Session["User"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult LoginClient(Client client)
        {
            Client c = ClientService.logClient(client.Email, client.Password);
            if (c != null)
            {
                Session["User"] = c;
                Response.Cookies["User"].Value = c.Id.ToString();
                Response.Cookies["User"].Expires = DateTime.Now.AddDays(1);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("LoginClient", "Home");
            }
        }

        [HttpPost]
        public ActionResult Index(ShoppingList sl)
        {
            if (Session["User"] != null)
            {
                Client Client = Session["User"] as Client;
            }
            else
            {
                return RedirectToAction("LoginClient", "Home");
            }

            sl.Finished = false;
            Client c = Session["User"] as Client;
            sl.ClientId = c.Id;
            if (sl.Private == null)
            {
                sl.Private = false;
            }
            if (sl.Name == null || sl.Name == "")
            {
                Random _rng = new Random();
                string _chars = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                int size = 7;
                char[] buffer = new char[size];

                for (int i = 0; i < size; i++)
                {
                    buffer[i] = _chars[_rng.Next(_chars.Length)];
                }
                sl.Name = new string(buffer);
            }

            ShoppingListService.AddNew(sl);
            List<ShoppingList> l = ShoppingListService.GetAll().ToList();
            ShoppingList flist = new ShoppingList();
            foreach (var item in l)
            {
                if (item.ClientId == sl.ClientId && item.Name == sl.Name)
                {
                    flist = item;
                    break;
                }
            }
            if (flist == null)
            {
                ViewBag.Error = "Failed to add list";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["ShoppingList"] = flist;
                return RedirectToAction("NewList", "Home");
            }
        }

        public ActionResult NewList()
        {
            if (Session["User"] != null)
            {
                Client Client = Session["User"] as Client;
            }
            else
            {
                return RedirectToAction("LoginClient", "Home");
            }

            if (Session["ShoppingList"] != null)
            {
                ShoppingList sl = Session["ShoppingList"] as ShoppingList;
                ItemShopping its = new ItemShopping();

                ViewBag.List = sl;
                return View();
            }
            else
            {
                ViewBag.Error = "Failed to add list";
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public ActionResult NewList(ItemShopping item)
        {
            if (Session["User"] != null)
            {
                Client Client = Session["User"] as Client;
            }
            else
            {
                return RedirectToAction("LoginClient", "Home");
            }
            if (Session["ShoppingList"] == null)
            {
                ViewBag.Error = "Failed to add list";
                return RedirectToAction("Index", "Home");
            }
            if (item.Product.Name != null && item.Product.Name != "")
            {
                int i = 0;
                List<Product> list = ProductService.GetAllProducts().ToList();
                foreach (Product p in list)
                {
                    if (p.Name.ToUpper().Contains(item.Product.Name.ToUpper()) || p.Description.ToUpper().Contains(item.Product.Name.ToUpper()))
                    {
                        ViewData.Add("Prod" + i, p);
                        i++;
                    }
                }
                if (i == 0)
                {
                    ViewBag.Error = "No items found.";
                }
            }
            else
            {
                ViewBag.Error = "No items found.";
                return View();
            }
            return View();
        }

        [HttpPost]
        public ActionResult addGenericList(String generic)
        {
            ShoppingList sl = Session["ShoppingList"] as ShoppingList;
            Client Client = Session["User"] as Client;

            ItemShopping s = new ItemShopping();
            s.Brand = generic;
            s.ShoppingListId = sl.Id;
            s.Quantity = 1;
            s.ProductId = null;
            ItemShoppingService.AddNew(s);
            return RedirectToAction("NewList", "Home");
        }

        [HttpPost]
        public ActionResult addCategoryList(String generic)
        {
            ShoppingList sl = Session["ShoppingList"] as ShoppingList;
            Client Client = Session["User"] as Client;

            ItemShopping s = new ItemShopping();
            s.Category = generic;
            s.ShoppingListId = sl.Id;
            s.Quantity = 1;
            s.ProductId = null;
            ItemShoppingService.AddNew(s);
            return RedirectToAction("NewList", "Home");
        }

        public ActionResult addProdList(int id)
        {
            if (Session["ShoppingList"] != null)
            {
                ShoppingList sl = Session["ShoppingList"] as ShoppingList;
                Client Client = Session["User"] as Client;
                ItemShopping s = new ItemShopping();
                s.ProductId = id;
                s.Product = ProductService.GetProductById(id);
                s.ShoppingListId = sl.Id;
                s.ShoppingList = sl;
                s.Quantity = 1;
                ItemShoppingService.AddNew(s);
            }
            return RedirectToAction("NewList", "Home");
        }

        public ActionResult ShoppingLists()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("LoginClient", "Home");
            }
            Client Client = Session["User"] as Client;
            var sl = ShoppingListService.GetByClientId(Client.Id);
            return View(sl);
        }

        public ActionResult ShoppingListDetails(int id)
        {
            List<ItemShopping> itemShopping = ItemShoppingService.GetAll().Where(i => i.ShoppingListId == id).ToList();
            List<ItemShopping> list = new List<ItemShopping>();
            foreach (var item in itemShopping)
            {
                if (item.ProductId != null)
                {
                    item.Product = ProductService.GetProductById((int)item.ProductId);
                    list.Add(item);
                }
            }
            return View(list);
        }

    }
}