using System;

namespace Borg.Framework.Cms.Annotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class TabAttribute : Attribute
    {
        public string Tab { get; set; }
    }
}