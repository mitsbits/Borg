﻿using Borg.Infrastructure.Core.Reflection.Discovery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Borg.System.Backoffice.Core.GenericEntity
{
    public class BackOfficeEntityControllerConstraint : IRouteConstraint
    {
        private List<string> Types = new List<string>(); private IAssemblyExplorerResult assemblyExplorerResult;

        public BackOfficeEntityControllerConstraint(IAssemblyExplorerResult assemblyExplorerResult)
        {
            var results = assemblyExplorerResult.Results<EntitiesAssemblyScanResult>().Where(x => x.Success).ToList();
            Types.AddRange(results.SelectMany(x => x.AllEntityTypes()).Distinct().Select(x => x.Name));
        }

        public bool Match(HttpContext httpContext,
            IRouter route,
            string routeKey,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            object routeValue;
            if (routeDirection == RouteDirection.IncomingRequest)
            {
                if (values.TryGetValue(routeKey, out routeValue))
                {
                    return Types.Any(x => x.Equals(routeValue.ToString(), StringComparison.InvariantCultureIgnoreCase));
                }
            }
            else
            {
            }

            return false;
        }
    }
}