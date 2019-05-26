using System;
using System.ComponentModel;

namespace Borg.Framework.EF.Instructions.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class KeySequenceDefinitionAttribute : Attribute
    {
        [DefaultValue("Id")]
        public string Column { get; set; } = "Id";
    }
}