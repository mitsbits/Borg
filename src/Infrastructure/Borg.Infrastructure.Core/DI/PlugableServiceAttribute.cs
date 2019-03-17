using System;
using System.ComponentModel;

namespace Borg.Infrastructure.Core.DI
{
    public class PlugableServiceAttribute : Attribute
    {
        private Type _implementationOf;

        public PlugableServiceAttribute(Type implementationOf, Lifetime lifetime = Lifetime.Transient, bool oneOfMany = false) : this()
        {
            ImplementationOf = implementationOf;
            Lifetime = lifetime;
            OneOfMany = oneOfMany;
        }

        public PlugableServiceAttribute()
        {
        }

        public Type ImplementationOf
        {
            get => _implementationOf; set
            {
                if (!value.IsInterface) throw new ArgumentException($"{nameof(value.FullName)} is not an Interface");
                _implementationOf = value;
            }
        }

        [DefaultValue(false)]
        public bool OneOfMany { get; set; } = false;

        [DefaultValue(Lifetime.Transient)]
        public Lifetime Lifetime { get; set; } = Lifetime.Transient;
    }
}