using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AgateNomad.App_Start;

namespace AgateNomad
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

    public class ControllerFactory : DefaultControllerFactory
    {
        public override IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }

            if (String.IsNullOrEmpty(controllerName))
            {
                throw new ArgumentException("MissingControllerName");
            }

            var controllerType = GetControllerType(requestContext, controllerName);

            // This is where a 404 is normally returned
            // Replaced with route to catchall controller
            if (controllerType == null)
            {
                // Build the dynamic route variable with all segments
                var dynamicRoute = "";
                foreach (var segment in requestContext.RouteData.Values.Values)
                {
                    dynamicRoute += segment + "/";
                }
                // Remove the last '/'
                dynamicRoute = dynamicRoute.Substring(0, dynamicRoute.Length - 1);

                // Route to the Catchall controller
                controllerName = "CatchAll";
                controllerType = GetControllerType(requestContext, controllerName);
                requestContext.RouteData.Values["Controller"] = controllerName;
                requestContext.RouteData.Values["action"] = "Index";
                requestContext.RouteData.Values["dynamicRoute"] = dynamicRoute;
            }

            IController controller = GetControllerInstance(requestContext, controllerType);
            return controller;
        }

    }
}