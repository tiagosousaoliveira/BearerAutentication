using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Sistema_Crud.API.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Sistema_Crud.API
{
    public class Startup
    {
        public void configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            configureWebApi(config);
            configureOAuth(app);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
        private void configureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuth = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/security/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(2),
                Provider = new AutenicationProviderServer()
            };

            app.UseOAuthAuthorizationServer(OAuth);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
        private void configureWebApi(HttpConfiguration config)
        {
            var JsonFormat = config.Formatters;
            JsonFormat.Remove(JsonFormat.XmlFormatter);

            var JsonSetings = JsonFormat.JsonFormatter.SerializerSettings;
            JsonSetings.Formatting = Formatting.Indented;

            JsonSetings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            JsonFormat.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(

                name:"DefaultRoute",
                routeTemplate:"api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional }
                
                );

        }
    }
}