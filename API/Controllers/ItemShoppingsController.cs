using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Data;
using Domain.Entities;

namespace API.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Domain.Entities;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<ItemShopping>("ItemShoppings");
    builder.EntitySet<Product>("Product"); 
    builder.EntitySet<ShoppingList>("ShoppingList"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ItemShoppingsController : ODataController
    {
        private Context db = new Context();

        // GET: odata/ItemShoppings
        [EnableQuery]
        public IQueryable<ItemShopping> GetItemShoppings()
        {
            return db.ItemShopping;
        }

        // GET: odata/ItemShoppings(5)
        [EnableQuery]
        public SingleResult<ItemShopping> GetItemShopping([FromODataUri] int key)
        {
            return SingleResult.Create(db.ItemShopping.Where(itemShopping => itemShopping.Id == key));
        }

        // PUT: odata/ItemShoppings(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<ItemShopping> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ItemShopping itemShopping = db.ItemShopping.Find(key);
            if (itemShopping == null)
            {
                return NotFound();
            }

            patch.Put(itemShopping);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemShoppingExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(itemShopping);
        }

        // POST: odata/ItemShoppings
        public IHttpActionResult Post(ItemShopping itemShopping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ItemShopping.Add(itemShopping);
            db.SaveChanges();

            return Created(itemShopping);
        }

        // PATCH: odata/ItemShoppings(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<ItemShopping> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ItemShopping itemShopping = db.ItemShopping.Find(key);
            if (itemShopping == null)
            {
                return NotFound();
            }

            patch.Patch(itemShopping);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemShoppingExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(itemShopping);
        }

        // DELETE: odata/ItemShoppings(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            ItemShopping itemShopping = db.ItemShopping.Find(key);
            if (itemShopping == null)
            {
                return NotFound();
            }

            db.ItemShopping.Remove(itemShopping);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ItemShoppings(5)/Product
        [EnableQuery]
        public SingleResult<Product> GetProduct([FromODataUri] int key)
        {
            return SingleResult.Create(db.ItemShopping.Where(m => m.Id == key).Select(m => m.Product));
        }

        // GET: odata/ItemShoppings(5)/ShoppingList
        [EnableQuery]
        public SingleResult<ShoppingList> GetShoppingList([FromODataUri] int key)
        {
            return SingleResult.Create(db.ItemShopping.Where(m => m.Id == key).Select(m => m.ShoppingList));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemShoppingExists(int key)
        {
            return db.ItemShopping.Count(e => e.Id == key) > 0;
        }
    }
}
