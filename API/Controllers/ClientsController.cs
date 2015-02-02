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
    builder.EntitySet<Client>("Clients");
    builder.EntitySet<Caddy>("Caddy"); 
    builder.EntitySet<ShoppingList>("ShoppingList"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ClientsController : ODataController
    {
        private Context db = new Context();

        // GET: odata/Clients
        [EnableQuery]
        public IQueryable<Client> GetClients()
        {
            return db.Client;
        }

        // GET: odata/Clients(5)
        [EnableQuery]
        public SingleResult<Client> GetClient([FromODataUri] int key)
        {
            return SingleResult.Create(db.Client.Where(client => client.Id == key));
        }

        // PUT: odata/Clients(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Client> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Client client = db.Client.Find(key);
            if (client == null)
            {
                return NotFound();
            }

            patch.Put(client);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(client);
        }

        // POST: odata/Clients
        public IHttpActionResult Post(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Client.Add(client);
            db.SaveChanges();

            return Created(client);
        }

        // PATCH: odata/Clients(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Client> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Client client = db.Client.Find(key);
            if (client == null)
            {
                return NotFound();
            }

            patch.Patch(client);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(client);
        }

        // DELETE: odata/Clients(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Client client = db.Client.Find(key);
            if (client == null)
            {
                return NotFound();
            }

            db.Client.Remove(client);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Clients(5)/Caddy
        [EnableQuery]
        public IQueryable<Caddy> GetCaddy([FromODataUri] int key)
        {
            return db.Client.Where(m => m.Id == key).SelectMany(m => m.Caddy);
        }

        // GET: odata/Clients(5)/ShoppingList
        [EnableQuery]
        public IQueryable<ShoppingList> GetShoppingList([FromODataUri] int key)
        {
            return db.Client.Where(m => m.Id == key).SelectMany(m => m.ShoppingList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int key)
        {
            return db.Client.Count(e => e.Id == key) > 0;
        }
    }
}
