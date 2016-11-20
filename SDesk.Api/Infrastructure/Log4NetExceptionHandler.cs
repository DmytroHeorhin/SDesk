using log4net;
using System.Web.Http.ExceptionHandling;

namespace SDesk.Api.Infrastructure
{
    public class Log4NetExceptionHandler : ExceptionHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override void Handle(ExceptionHandlerContext context)
        {
            Log.Error(context.Exception.Message);
            base.Handle(context);
        }
    }
}