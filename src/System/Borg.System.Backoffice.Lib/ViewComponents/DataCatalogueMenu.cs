using Borg.Framework.Cms;
using Borg.Framework.Services.AssemblyScanner;
using Borg.Infrastructure.Core;
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
        private static IDictionary<string, string> _cache;

        public DataCatalogueMenu(IEnumerable<IAssemblyProvider> assemblyProviders)
        {
            Preconditions.NotNull(assemblyProviders, nameof(assemblyProviders));
            Preconditions.NotEmpty(assemblyProviders, nameof(assemblyProviders));
            this.assemblyProviders = assemblyProviders;
        }

        public IViewComponentResult Invoke()
        {
            if (_cache == null)
            {
                var types = assemblyProviders.SelectMany(x => x.GetAssemblies()).SelectMany(x => x.GetTypes())
                        .Where(x => x.GetCustomAttribute<GenericEntityAttribute>() != null).OrderBy(x => x.FullName).Distinct();

                _cache = types.ToDictionary(x => Url.RouteUrl(new { Controller = x.Name }), x => EntityPluralTitle(x));
            }

            return View(_cache);
        }

        private static string EntityPluralTitle(Type type)
        {
            var attr = type.GetCustomAttribute<GenericEntityAttribute>();
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