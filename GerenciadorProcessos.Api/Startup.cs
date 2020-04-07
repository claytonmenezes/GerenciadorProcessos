using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

namespace GerenciadorProcessos.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //config
            HttpConfiguration config = new HttpConfiguration();
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            ConfigureWebApi(config);

            ConfigureOAuth(app);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
            Infra.StartupJob.StartScheduler();
        }
        public static void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/security/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(365),
                Provider = new AuthorizationServerProvider()
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}