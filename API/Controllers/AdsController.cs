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
    builder.EntitySet<Ad>("Ads");
    builder.EntitySet<Product>("Product"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AdsController : ODataController
    {
        private Context db = new Context();

        // GET: odata/Ads
        [EnableQuery]
        public IQueryable<Ad> GetAds()
        {
            return db.Ad;
        }

        // GET: odata/Ads(5)
        [EnableQuery]
        public SingleResult<Ad> GetAd([FromODataUri] int key)
        {
            return SingleResult.Create(db.Ad.Where(ad => ad.Id == key));
        }

        // PUT: odata/Ads(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Ad> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Ad ad = db.Ad.Find(key);
            if (ad == null)
            {
                return NotFound();
            }

            patch.Put(ad);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(ad);
        }

        // POST: odata/Ads
        public IHttpActionResult Post(Ad ad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ad.Add(ad);
            db.SaveChanges();

            return Created(ad);
        }

        // PATCH: odata/Ads(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Ad> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Ad ad = db.Ad.Find(key);
            if (ad == null)
            {
                return NotFound();
            }

            patch.Patch(ad);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(ad);
        }

        // DELETE: odata/Ads(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Ad ad = db.Ad.Find(key);
            if (ad == null)
            {
                return NotFound();
            }

            db.Ad.Remove(ad);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Ads(5)/Product
        [EnableQuery]
        public SingleResult<Product> GetProduct([FromODataUri] int key)
        {
            return SingleResult.Create(db.Ad.Where(m => m.Id == key).Select(m => m.Product));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdExists(int key)
        {
            return db.Ad.Count(e => e.Id == key) > 0;
        }
    }
}
