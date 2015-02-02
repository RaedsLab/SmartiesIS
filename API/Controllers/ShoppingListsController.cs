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
    builder.EntitySet<ShoppingList>("ShoppingLists");
    builder.EntitySet<Client>("Client"); 
    builder.EntitySet<ItemShopping>("ItemShopping"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ShoppingListsController : ODataController
    {
        private Context db = new Context();

        // GET: odata/ShoppingLists
        [EnableQuery]
        public IQueryable<ShoppingList> GetShoppingLists()
        {
            return db.ShoppingList;
        }

        // GET: odata/ShoppingLists(5)
        [EnableQuery]
        public SingleResult<ShoppingList> GetShoppingList([FromODataUri] int key)
        {
            return SingleResult.Create(db.ShoppingList.Where(shoppingList => shoppingList.Id == key));
        }

        // PUT: odata/ShoppingLists(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<ShoppingList> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ShoppingList shoppingList = db.ShoppingList.Find(key);
            if (shoppingList == null)
            {
                return NotFound();
            }

            patch.Put(shoppingList);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingListExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(shoppingList);
        }

        // POST: odata/ShoppingLists
        public IHttpActionResult Post(ShoppingList shoppingList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ShoppingList.Add(shoppingList);
            db.SaveChanges();

            return Created(shoppingList);
        }

        // PATCH: odata/ShoppingLists(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<ShoppingList> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ShoppingList shoppingList = db.ShoppingList.Find(key);
            if (shoppingList == null)
            {
                return NotFound();
            }

            patch.Patch(shoppingList);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingListExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(shoppingList);
        }

        // DELETE: odata/ShoppingLists(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            ShoppingList shoppingList = db.ShoppingList.Find(key);
            if (shoppingList == null)
            {
                return NotFound();
            }

            db.ShoppingList.Remove(shoppingList);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ShoppingLists(5)/Client
        [EnableQuery]
        public SingleResult<Client> GetClient([FromODataUri] int key)
        {
            return SingleResult.Create(db.ShoppingList.Where(m => m.Id == key).Select(m => m.Client));
        }

        // GET: odata/ShoppingLists(5)/ItemShopping
        [EnableQuery]
        public IQueryable<ItemShopping> GetItemShopping([FromODataUri] int key)
        {
            return db.ShoppingList.Where(m => m.Id == key).SelectMany(m => m.ItemShopping);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShoppingListExists(int key)
        {
            return db.ShoppingList.Count(e => e.Id == key) > 0;
        }
    }
}
