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
    builder.EntitySet<Caddy>("Caddies");
    builder.EntitySet<Client>("Client"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class CaddiesController : ODataController
    {
        private Context db = new Context();

        // GET: odata/Caddies
        [EnableQuery]
        public IQueryable<Caddy> GetCaddies()
        {
            return db.Caddy;
        }

        // GET: odata/Caddies(5)
        [EnableQuery]
        public SingleResult<Caddy> GetCaddy([FromODataUri] int key)
        {
            return SingleResult.Create(db.Caddy.Where(caddy => caddy.Id == key));
        }

        // PUT: odata/Caddies(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Caddy> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Caddy caddy = db.Caddy.Find(key);
            if (caddy == null)
            {
                return NotFound();
            }

            patch.Put(caddy);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaddyExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(caddy);
        }

        // POST: odata/Caddies
        public IHttpActionResult Post(Caddy caddy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Caddy.Add(caddy);
            db.SaveChanges();

            return Created(caddy);
        }

        // PATCH: odata/Caddies(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Caddy> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Caddy caddy = db.Caddy.Find(key);
            if (caddy == null)
            {
                return NotFound();
            }

            patch.Patch(caddy);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaddyExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(caddy);
        }

        // DELETE: odata/Caddies(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Caddy caddy = db.Caddy.Find(key);
            if (caddy == null)
            {
                return NotFound();
            }

            db.Caddy.Remove(caddy);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Caddies(5)/Client
        [EnableQuery]
        public SingleResult<Client> GetClient([FromODataUri] int key)
        {
            return SingleResult.Create(db.Caddy.Where(m => m.Id == key).Select(m => m.Client));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CaddyExists(int key)
        {
            return db.Caddy.Count(e => e.Id == key) > 0;
        }
    }
}
