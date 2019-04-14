using System;

namespace Borg.Framework.MVC
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class GeneratedControllerAttribute : Attribute
    {
        public GeneratedControllerAttribute(string route)
        {
            Route = route;
        }

        public string Route { get; set; }
    }

    //public class GenericTypeControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    //{
    //    public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
    //    {
    //        var currentAssembly = typeof(GenericTypeControllerFeatureProvider).Assembly;
    //        var candidates = currentAssembly.GetExportedTypes().Where(x => x.GetCustomAttributes<GeneratedControllerAttribute>().Any());

    //        foreach (var candidate in candidates)
    //        {
    //            feature.Controllers.Add(
    //                typeof(BaseController<>).MakeGenericType(candidate).GetTypeInfo()
    //            );
    //        }
    //    }
    //}
}