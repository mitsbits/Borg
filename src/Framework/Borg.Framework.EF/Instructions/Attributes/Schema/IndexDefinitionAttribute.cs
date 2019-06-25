using System;
using System.ComponentModel;

namespace Borg.Framework.EF.Instructions.Attributes.Schema
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class PrimaryKeyDefinitionAttribute : UniqueIndexDefinitionAttribute
    {
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class UniqueIndexDefinitionAttribute : IndexDefinitionAttribute
    {
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class IndexDefinitionAttribute : Attribute
    {
        public const string DefaultIndexName = nameof(IndexName);
        public string IndexName { get; set; } = DefaultIndexName;

        [DefaultValue(0)]
        public int Order { get; set; } = 0;

        public enum IndexDefinitionMode
        {
            Index, UniqueIndex, PrimaryKey
        }
    }
}