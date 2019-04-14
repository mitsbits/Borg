using System;

namespace Borg.Framework.EF.Instructions.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class PrimaryKeyDefinitionAttribute : Attribute
    {
        public PrimaryKeyDefinitionAttribute(int order = 0)
        {
            Order = Order;
        }
        public int Order { get; }
    }
}
