using System;

namespace Borg.Framework.EF.Instructions.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class KeySequenceDefinitionAttribute : Attribute
    {
        public KeySequenceDefinitionAttribute(string column = null)
        {
            Column = column.IsNullOrWhiteSpace() ? "Id" : column;
        }

        public string Column { get; }
    }
}