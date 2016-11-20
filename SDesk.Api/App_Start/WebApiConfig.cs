using System.Web.Http;
using System.Web.Http.Routing;
using SDesk.Api.RoutingConstraints;
using System.Web.Http.ExceptionHandling;
using SDesk.Api.Infrastructure;

namespace SDesk.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var constraintResolver = new DefaultInlineConstraintResolver();
            constraintResolver.ConstraintMap.Add("jiraid", typeof(JiraIdConstraint));
            config.MapHttpAttributeRoutes(constraintResolver);

            log4net.Config.XmlConfigurator.Configure();
            config.Services.Replace(typeof(IExceptionHandler), new Log4NetExceptionHandler());

            config.Routes.MapHttpRoute(
                name: "JiraIdConstraintBasedApi",
                routeTemplate: "api/jiraitems/{id}",
                defaults: new { controller = "jiraitems", action = "JiraItemString" },                                                 
                constraints: new { id = new JiraIdConstraint() });
          
            config.Routes.MapHttpRoute(
                name: "DigitalIdConstraintBasedApi",
                routeTemplate: "api/jiraitems/{id}",
                defaults: new { controller = "jiraitems", action = "JiraItemInt", id = 1 }); 
                
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional});                                       
        }
    }
}
