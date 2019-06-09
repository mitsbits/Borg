using Borg.Framework.Cms.Annotations;

using Borg.Infrastructure.Core;
using Borg.Infrastructure.Core.Reflection.Discovery;
using Borg.Platform.EF.Instructions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Borg.System.Backoffice.Lib.ViewComponents
{
    public class DataCatalogueMenu : ViewComponent
    {
        private readonly IEnumerable<IAssemblyProvider> assemblyProviders;
        private static IDictionary<string, IDictionary<string, string>> _cache;

        public DataCatalogueMenu(IEnumerable<IAssemblyProvider> assemblyProviders)
        {
            assemblyProviders = Preconditions.NotNull(assemblyProviders, nameof(assemblyProviders));
            this.assemblyProviders = Preconditions.NotEmpty(assemblyProviders, nameof(assemblyProviders)); ;
        }

        public IViewComponentResult Invoke()
        {
            if (_cache == null)
            {
                _cache = new Dictionary<string, IDictionary<string, string>>();
                var types = assemblyProviders.SelectMany(x => x.GetAssemblies()).SelectMany(x => x.GetTypes())
                        .Where(x => x.IsCmsAggregateRoot()).OrderBy(x => x.FullName).Distinct();
                var maps = assemblyProviders.SelectMany(x => x.GetAssemblies()).SelectMany(x => x.GetTypes()).
                    Where(x => x.IsSubclassOfRawGeneric(typeof(EntityMapBase<,>)) && !x.IsAbstract);

                var grouprdMaps = maps.GroupBy(x => x.BaseType.GetGenericArguments()[1]);
                foreach (var group in grouprdMaps)
                {
                    var innerDict = new Dictionary<string, string>();
                    foreach (var type in types)
                    {
                        if (group.Any(x => x.BaseType.GetGenericArguments()[0] == type))
                        {
                            innerDict.Add(Url.RouteUrl(new { Controller = type.Name }), EntityPluralTitle(type));
                        }
                    }
                    if (innerDict.Count() > 0)
                    {
                        _cache.Add(group.Key.Name.SplitCamelCaseToWords(), innerDict);
                    }
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