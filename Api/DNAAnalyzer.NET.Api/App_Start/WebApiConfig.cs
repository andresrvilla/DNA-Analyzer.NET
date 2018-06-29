using System.Web.Http;

namespace DNAAnalyzer.NET.Api.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Mutant",
                routeTemplate: "mutant",
                defaults: new { controller = "Mutant" });

            config.Routes.MapHttpRoute(
                name: "Stats",
                routeTemplate: "stats",
                defaults: new { controller = "Stats" });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            config.EnableSystemDiagnosticsTracing();
        }
    }
}