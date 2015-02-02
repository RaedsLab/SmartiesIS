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
    builder.EntitySet<Product>("Products");
    builder.EntitySet<Ad>("Ad"); 
    builder.EntitySet<Ayle>("Ayle"); 
    builder.EntitySet<ItemShopping>("ItemShopping"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ProductsController : ODataController
    {
        private Context db = new Context();

        // GET: odata/Products
        [EnableQuery]
        public IQueryable<Product> GetProducts()
        {
            return db.Product;
        }

        // GET: odata/Products(5)
        [EnableQuery]
        public SingleResult<Product> GetProduct([FromODataUri] int key)
        {
            return SingleResult.Create(db.Product.Where(product => product.Id == key));
        }

        // PUT: odata/Products(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Product> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product product = db.Product.Find(key);
            if (product == null)
            {
                return NotFound();
            }

            patch.Put(product);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(product);
        }

        // POST: odata/Products
        public IHttpActionResult Post(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Product.Add(product);
            db.SaveChanges();

            return Created(product);
        }

        // PATCH: odata/Products(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Product> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product product = db.Product.Find(key);
            if (product == null)
            {
                return NotFound();
            }

            patch.Patch(product);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(product);
        }

        // DELETE: odata/Products(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Product product = db.Product.Find(key);
            if (product == null)
            {
                return NotFound();
            }

            db.Product.Remove(product);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Products(5)/Ad
        [EnableQuery]
        public IQueryable<Ad> GetAd([FromODataUri] int key)
        {
            return db.Product.Where(m => m.Id == key).SelectMany(m => m.Ad);
        }

        // GET: odata/Products(5)/Ayle
        [EnableQuery]
        public SingleResult<Ayle> GetAyle([FromODataUri] int key)
        {
            return SingleResult.Create(db.Product.Where(m => m.Id == key).Select(m => m.Ayle));
        }

        // GET: odata/Products(5)/ItemShopping
        [EnableQuery]
        public IQueryable<ItemShopping> GetItemShopping([FromODataUri] int key)
        {
            return db.Product.Where(m => m.Id == key).SelectMany(m => m.ItemShopping);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int key)
        {
            return db.Product.Count(e => e.Id == key) > 0;
        }
    }
}
