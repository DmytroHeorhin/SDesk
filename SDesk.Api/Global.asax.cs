using System.Web;
using System.Web.Http;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace SDesk.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
