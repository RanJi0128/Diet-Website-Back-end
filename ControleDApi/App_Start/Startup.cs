using ControleDApi.DAL;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Owin.Cors;

namespace ControleDApi.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();
            
            //ConfigureRotas(configuration);
            //configuration.EnableCors();
            
            app.CreatePerOwinContext(Context.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);


            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/Login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new ControleDAuthorizationServerProvider(),
            };
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(configuration);
        }
        private static void ConfigureRotas(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}