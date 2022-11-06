using log4net;
using System.Text;
using System.Web.Http.ExceptionHandling;

namespace WebApplication.DotNetFrameWork.ExceptionHandlers
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(GlobalExceptionLogger));

        public GlobalExceptionLogger()
        {
        }

        public override void Log(ExceptionLoggerContext context)
        {
            StringBuilder builder = new StringBuilder();
            //string controller = context.ExceptionContext.ControllerContext?
            //    .ControllerDescriptor?.ControllerName;
            //string action = context.ExceptionContext.ActionContext?
            //    .ActionDescriptor?.ActionName;
            //builder.AppendFormat("An error '{0}' when processing action '{1}' in controller '{2}'"
            //    , context.ExceptionContext.Exception.Message
            //    , action
            //    , controller);
            builder.AppendFormat("An error '' while processing request '{0}' "
                , context.ExceptionContext.Exception.Message
                , context.Request.RequestUri);
            log.Fatal(builder.ToString(), context.ExceptionContext.Exception);
            base.Log(context);
        }
    }
}