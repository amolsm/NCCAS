using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using SchoolManagementSystems.App_Start;
using System.Web.Optimization;
using log4net;
namespace SchoolManagementSystems
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MvcApplication));
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Application_Error()
        {
            Exception ex = Server.GetLastError().GetBaseException();

            log.Error("App_Error", ex);
            Server.ClearError();
            Response.Redirect("~/Login/Login");
        }

        public class SessionExpireAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                HttpContext ctx = HttpContext.Current;
                // check  sessions here
                if (HttpContext.Current.Session["Userid"] == null)
                {
                    filterContext.Result = new RedirectResult("~/Login/Login");
                    return;
                }
                base.OnActionExecuting(filterContext);
            }
        }
    }
}