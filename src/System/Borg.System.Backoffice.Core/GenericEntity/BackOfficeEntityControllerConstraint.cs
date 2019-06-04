using Borg.Framework.Services.AssemblyScanner;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;

namespace Borg.System.Backoffice.Core.GenericEntity
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
                              .Where(t => t.IsCmsAggregateRoot()).Distinct());
                return types.Any(x => x.Name == routeValue.ToString());
            }

            return false;
        }
    }
}