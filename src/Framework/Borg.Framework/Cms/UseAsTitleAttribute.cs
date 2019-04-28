using System;

namespace Borg.Framework.Cms
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class UseAsTitleAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class PropertyTabAttribute : Attribute
    {
        private const string defaultTabHeader = "General";
        public string Header { get; set; } = defaultTabHeader;

        public PropertyTabAttribute()
        {

        }
        public PropertyTabAttribute(string header)
        {
            Header = header;
        }
    }
}