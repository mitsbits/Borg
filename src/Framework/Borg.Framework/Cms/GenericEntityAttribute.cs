using System;

namespace Borg.Framework.Cms
{
    public class GenericEntityAttribute : Attribute
    {
        public string Plural { get; set; }
        public string Singular { get; set; }
    }
}