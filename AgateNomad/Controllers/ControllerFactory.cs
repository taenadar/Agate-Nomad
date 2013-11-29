using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace AgateNomad.Controllers
{
    public class ControllerFactory : DefaultControllerFactory
    {
        public override IController CreateController(RequestContext requestContext, string controllerName)
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