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
    builder.EntitySet<Admin>("Admins");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AdminsController : ODataController
    {
        private Context db = new Context();

        // GET: odata/Admins
        [EnableQuery]
        public IQueryable<Admin> GetAdmins()
        {
            return db.Admin;
        }

        // GET: odata/Admins(5)
        [EnableQuery]
        public SingleResult<Admin> GetAdmin([FromODataUri] int key)
        {
            return SingleResult.Create(db.Admin.Where(admin => admin.Id == key));
        }

        // PUT: odata/Admins(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Admin> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Admin admin = db.Admin.Find(key);
            if (admin == null)
            {
                return NotFound();
            }

            patch.Put(admin);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(admin);
        }

        // POST: odata/Admins
        public IHttpActionResult Post(Admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Admin.Add(admin);
            db.SaveChanges();

            return Created(admin);
        }

        // PATCH: odata/Admins(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Admin> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Admin admin = db.Admin.Find(key);
            if (admin == null)
            {
                return NotFound();
            }

            patch.Patch(admin);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(admin);
        }

        // DELETE: odata/Admins(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Admin admin = db.Admin.Find(key);
            if (admin == null)
            {
                return NotFound();
            }

            db.Admin.Remove(admin);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdminExists(int key)
        {
            return db.Admin.Count(e => e.Id == key) > 0;
        }
    }
}
