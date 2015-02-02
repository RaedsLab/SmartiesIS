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
    builder.EntitySet<Ayle>("Ayles");
    builder.EntitySet<Product>("Product"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AylesController : ODataController
    {
        private Context db = new Context();

        // GET: odata/Ayles
        [EnableQuery]
        public IQueryable<Ayle> GetAyles()
        {
            return db.Ayle;
        }

        // GET: odata/Ayles(5)
        [EnableQuery]
        public SingleResult<Ayle> GetAyle([FromODataUri] int key)
        {
            return SingleResult.Create(db.Ayle.Where(ayle => ayle.Id == key));
        }

        // PUT: odata/Ayles(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Ayle> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Ayle ayle = db.Ayle.Find(key);
            if (ayle == null)
            {
                return NotFound();
            }

            patch.Put(ayle);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AyleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(ayle);
        }

        // POST: odata/Ayles
        public IHttpActionResult Post(Ayle ayle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ayle.Add(ayle);
            db.SaveChanges();

            return Created(ayle);
        }

        // PATCH: odata/Ayles(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Ayle> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Ayle ayle = db.Ayle.Find(key);
            if (ayle == null)
            {
                return NotFound();
            }

            patch.Patch(ayle);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AyleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(ayle);
        }

        // DELETE: odata/Ayles(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Ayle ayle = db.Ayle.Find(key);
            if (ayle == null)
            {
                return NotFound();
            }

            db.Ayle.Remove(ayle);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Ayles(5)/Product
        [EnableQuery]
        public IQueryable<Product> GetProduct([FromODataUri] int key)
        {
            return db.Ayle.Where(m => m.Id == key).SelectMany(m => m.Product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AyleExists(int key)
        {
            return db.Ayle.Count(e => e.Id == key) > 0;
        }
    }
}
