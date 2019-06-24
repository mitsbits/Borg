using System;

namespace Borg.Infrastructure.Core.Reflection.Discovery.Annotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class MapperIgnoreAttribute : Attribute
    {
    }
}