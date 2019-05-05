using Borg.Infrastructure.Core;
using Microsoft.AspNetCore.Mvc;

namespace Borg
{
    public static partial class ControllerContextExtensions
    {
        public static string Id(this ControllerContext controllerContext)
        {
            controllerContext = Preconditions.NotNull(controllerContext, nameof(controllerContext));
            var routeValues = controllerContext.RouteData.Values;

            if (routeValues.ContainsKey("id")) return (string)routeValues["id"];
            else if (controllerContext.HttpContext.Request.Query.ContainsKey("id"))
                return controllerContext.HttpContext.Request.Query["id"];

            return string.Empty;
        }

        public static string Controller(this ControllerContext controllerContext)
        {
            controllerContext = Preconditions.NotNull(controllerContext, nameof(controllerContext));
            var routeValues = controllerContext.RouteData.Values;

            if (routeValues.ContainsKey("controller")) return (string)routeValues["controller"];

            return string.Empty;
        }

        public static string Action(this ControllerContext controllerContext)
        {
            controllerContext = Preconditions.NotNull(controllerContext, nameof(controllerContext));
            var routeValues = controllerContext.RouteData.Values;

            if (routeValues.ContainsKey("action")) return (string)routeValues["action"];

            return string.Empty;
        }
    }
}