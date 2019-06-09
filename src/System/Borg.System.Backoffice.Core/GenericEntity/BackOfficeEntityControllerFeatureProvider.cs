using Borg.Infrastructure.Core.Reflection.Discovery;
using Borg.Platform.EF.Instructions;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Borg.System.Backoffice.Core.GenericEntity
{
    public class BackOfficeEntityControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private readonly IEnumerable<IAssemblyProvider> assemblyProviders;

        public BackOfficeEntityControllerFeatureProvider(IEnumerable<IAssemblyProvider> assemblyProviders)
        {
            this.assemblyProviders = assemblyProviders;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var types = assemblyProviders.SelectMany(x => x.GetAssemblies()).SelectMany(x => x.GetTypes()
            .Where(t => t.IsCmsAggregateRoot()).Distinct());
            foreach (var entityType in types)
            {
                var typeName = entityType.Name + "Controller";
                var tps = entityType.Assembly.GetTypes().Where(t => t.IsSubclassOfRawGeneric(typeof(EntityMapBase<,>))).Select(x => x.BaseType).ToList();
                var map = tps.FirstOrDefault(t => t.GetTypeInfo().GenericTypeArguments[0] == entityType);
                if (map != null)
                {
                    var dbtype = map.GetGenericArguments()[1];

                    // Check to see if there is a "real" controller for this class
                    if (!feature.Controllers.Any(t => t.Name == typeName))
                    {
                        // Create a generic controller for this type
                        var controllerType = typeof(BackOfficeEntityController<,>).MakeGenericType(entityType, dbtype).GetTypeInfo();
                        feature.Controllers.Add(controllerType);
                    }
                }
            }
        }
    }
}