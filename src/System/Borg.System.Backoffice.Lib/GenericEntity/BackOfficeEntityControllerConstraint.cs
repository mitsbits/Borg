using Borg.Framework.Cms;
using Borg.Framework.Services.AssemblyScanner;
using Borg.Infrastructure.Core.DDD.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Borg.System.Backoffice.Lib
{
    public class BackOfficeEntityControllerConstraint : IRouteConstraint
    {
        private readonly IEnumerable<IAssemblyProvider> assemblyProviders;

        public BackOfficeEntityControllerConstraint(IEnumerable<IAssemblyProvider> assemblyProviders)
        {
            this.assemblyProviders = assemblyProviders;
        }

        public bool Match(HttpContext httpContext,
            IRouter route,
            string routeKey,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            object routeValue;
            if (values.TryGetValue(routeKey, out routeValue))
            {
                var types = assemblyProviders.SelectMany(x => x.GetAssemblies()).SelectMany(x => x.GetTypes()
                              .Where(t => t.ImplementsInterface(typeof(IEntity)) && !t.IsAbstract && t.GetCustomAttribute<CmsEntityAttribute>() != null).Distinct());
                return types.Any(x => x.Name == routeValue.ToString());
            }

            return false;
        }
    }
}