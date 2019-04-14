using System;

namespace Borg.Platform.EF.Instructions.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TableSchemaDefinitionAttribute : Attribute
    {
        public TableSchemaDefinitionAttribute(string schema)
        {
            Schema = schema;
        }

        public string Schema { get; }
    }
}