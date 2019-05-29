using System;
using System.Collections.Generic;
using System.Linq;

namespace Borg.Framework.Reflection.ObjectGraph
{
    public class ComplexTypeRecursorResult : List<ComplexTypeRecursorResultItem>
    {
        internal void Add(Type type, Type referrer = null, int recursion = 0)
        {
            Add(new ComplexTypeRecursorResultItem(type, referrer, recursion));
        }

        public Type[] Entities()
        {
            return this.Select(x => x.Type).Distinct().ToArray();
        }
    }
}