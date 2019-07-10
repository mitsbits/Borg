using Borg.Framework.Cms.Annotations;
using Borg.Infrastructure.Core;
using Borg.Infrastructure.Core.Reflection.Discovery;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Borg.System.Backoffice.Lib.ViewComponents
{
    public class DataCatalogueMenu : ViewComponent
    {
        private readonly IAssemblyExplorerResult assemblyExplorerResult;
        private static IDictionary<string, IDictionary<string, string>> _cache;

        public DataCatalogueMenu(IAssemblyExplorerResult assemblyExplorerResult)
        {
            this.assemblyExplorerResult = Preconditions.NotNull(assemblyExplorerResult, nameof(assemblyExplorerResult)); ;
        }

        public IViewComponentResult Invoke()
        {
            if (_cache == null)
            {
                _cache = new Dictionary<string, IDictionary<string, string>>();
                var results = assemblyExplorerResult.Results<EntitiesAssemblyScanResult>();

                var alltypes = new List<Type>();

                foreach (var result in results.Where(x => x.Success))
                {
                    var locals = result.AllEntityTypes().Distinct().ToArray();
                    var dict = new Dictionary<string, string>();
                    foreach (var local in locals)
                    {
                        var key = Url.Link("areaGenericEntityRoute", new { Controller = local.Name, Action = "Index" });
                        var label = EntityPluralTitle(local);
                        if (!dict.ContainsKey(key))
                        {
                            dict.Add(key, label);
                        }
                        else
                        {
                            dict[key] = label;
                        }
                    }

                    _cache.Add(result.DefaultDbs?.First().Name, dict);
                }
            }

            return View(_cache);
        }

        private static string EntityPluralTitle(Type type)
        {
            var attr = type.GetCustomAttribute<CmsAggregateRootAttribute>();
            if (attr != null)
            {
                if (attr.Plural.IsNullOrWhiteSpace())
                {
                    return type.Name.SplitCamelCaseToWords();
                }

                return attr.Plural;
            }
            return type.Name.SplitCamelCaseToWords();
        }
    }
}