using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
////
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using Domain.Entities;

namespace API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ////
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Ad>("Ads");
            builder.EntitySet<Admin>("Admins");
            builder.EntitySet<Ayle>("Ayles");
            builder.EntitySet<Client>("Clients");
            builder.EntitySet<Caddy>("Caddies");
            builder.EntitySet<ShoppingList>("ShoppingLists");
            builder.EntitySet<ItemShopping>("ItemShoppings");
            builder.EntitySet<Product>("Products");

            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());


            builder.EntitySet<Client>("Clients").EntityType.HasKey(o => o.Id);


            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
