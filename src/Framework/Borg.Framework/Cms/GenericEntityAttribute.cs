using System;

namespace Borg.Framework.Cms
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class GenericEntityAttribute : Attribute
    {
        public string Plural { get; set; }
        public string Singular { get; set; }
    }
}