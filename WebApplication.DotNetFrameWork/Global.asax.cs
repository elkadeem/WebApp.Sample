using log4net.Config;
using log4net.Core;
using Swashbuckle.Swagger;
using System.Diagnostics;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApplication.DotNetFrameWork
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            XmlConfigurator.Configure();
            log4net.GlobalContext.Properties["pid"] = Process.GetCurrentProcess().Id;
        }
        protected void Application_BeginRequest()
        {
            log4net.LogicalThreadContext.Properties["activityid"] = new ActivityIdHelper();
        }

        protected void Application_End()
        {
            LoggerManager.Shutdown();
        }
    }

    public class ActivityIdHelper
    {
        public override string ToString()
        {
            if (Trace.CorrelationManager.ActivityId == Guid.Empty)
            {
                Trace.CorrelationManager.ActivityId = Guid.NewGuid();
            }

            return Trace.CorrelationManager.ActivityId.ToString();
        }
    }
}
