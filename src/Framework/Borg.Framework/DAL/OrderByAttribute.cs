using System;

namespace Borg.Framework.DAL
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class OrderByAttribute : Attribute
    {
        public bool Ascending { get; set; } = true;

        public int Precedence { get; set; } = 0;
    }
}