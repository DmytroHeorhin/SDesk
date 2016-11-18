using System.Web.Http;
using Owin;
using SDesk.Api;

namespace SDesc.Api.SelfHost
{
    public class Startup
    {        
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            appBuilder.UseWebApi(config);
        }
    }  
}
