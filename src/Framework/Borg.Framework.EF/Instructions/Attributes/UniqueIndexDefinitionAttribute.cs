using System;
using System.ComponentModel;

namespace Borg.Framework.EF.Instructions.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class UniqueIndexDefinitionAttribute : Attribute
    {
        public string IndexName { get; } = nameof(IndexName);

        [DefaultValue(0)]
        public int Order { get; } = 0;
    }
}