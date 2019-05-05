using Borg.Infrastructure.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Borg
{
    public static partial class IHtmlHelperExtensions
    {
        public static string Id(this IHtmlHelper htmlHelper)
        {
            htmlHelper = Preconditions.NotNull(htmlHelper, nameof(htmlHelper));
            var routeValues = htmlHelper.ViewContext.RouteData.Values;

            if (routeValues.ContainsKey("id"))
                return (string)routeValues["id"];
            else if (htmlHelper.ViewContext.HttpContext.Request.Query.ContainsKey("id"))
                return htmlHelper.ViewContext.HttpContext.Request.Query["id"];

            return string.Empty;
        }

        public static string Controller(this HtmlHelper htmlHelper)
        {
            htmlHelper = Preconditions.NotNull(htmlHelper, nameof(htmlHelper));
            var routeValues = htmlHelper.ViewContext.RouteData.Values;

            if (routeValues.ContainsKey("controller"))
                return (string)routeValues["controller"];

            return string.Empty;
        }

        public static string Action(this HtmlHelper htmlHelper)
        {
            htmlHelper = Preconditions.NotNull(htmlHelper, nameof(htmlHelper));
            var routeValues = htmlHelper.ViewContext.RouteData.Values;

            if (routeValues.ContainsKey("action"))
                return (string)routeValues["action"];

            return string.Empty;
        }
    }
}