using System;

namespace Borg.Framework.EF.Instructions.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class SequenceDefinitionAttribute : Attribute
    {
    }
}