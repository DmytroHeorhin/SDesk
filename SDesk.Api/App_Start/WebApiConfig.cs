using System.Web.Http;
using System.Web.Http.Routing;
using SDesk.Api.RoutingConstraints;

namespace SDesk.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var constraintResolver = new DefaultInlineConstraintResolver();
            constraintResolver.ConstraintMap.Add("jiraid", typeof(JiraIdConstraint));
            config.MapHttpAttributeRoutes(constraintResolver);

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
