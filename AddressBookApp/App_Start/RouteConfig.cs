using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AddressBookApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AddressesCreate",
                url: "Contacts/{id}/Addresses/Create",
                defaults: new { controller = "Addresses", action = "Create" }
            );

            routes.MapRoute(
                name: "EmailsCreate",
                url: "Contacts/{id}/Emails/Create",
                defaults: new { controller = "Emails", action = "Create" }
            );            

            routes.MapRoute(
                name: "Emails",
                url: "Contacts/{id}/Emails/{emailId}/{action}",
                defaults: new { controller = "Emails", action = "Edit" }
            );

            routes.MapRoute(
                name: "Addresses",
                url: "Contacts/{id}/Addresses/{addressId}/{action}",
                defaults: new { controller = "Addresses", action = "Edit"}
            );          

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Contacts", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
