using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ControleDApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)

        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //var corsAttr = new EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(corsAttr);
            config.EnableCors();

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());


            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApiAction",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
