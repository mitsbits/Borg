using Borg.Infrastructure.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;

namespace Borg.Framework.Reflection.ObjectGraph
{
    internal class ComplexTypeRecursor : IDisposable
    {
        private readonly ComplexTypeRecursorResult source;
        private readonly ILogger logger;
        private int recursionLevel;
        private Type currentReferer;

        internal ComplexTypeRecursor(Type root, ILogger logger = null)
        {
            source = new ComplexTypeRecursorResult();
            this.logger = logger ?? NullLogger.Instance;
            recursionLevel = 0;
        }

        internal ComplexTypeRecursorResult Results()
        {
            var bucket = Preconditions.NotNull(source, nameof(source));
            return bucket;
        }

        private void DiscoverComplexTypes(Type root)
        {
            logger.Debug($"{nameof(ComplexTypeRecursor)} discovering {root.FullName} | Recursion {recursionLevel}");
            source.Add(root, currentReferer, recursionLevel);
            recursionLevel++;
            currentReferer = root;
            foreach (var prop in root.GetProperties())
            {
                if (prop.IsSimple()) continue;
                Type target = null;
                if (prop.IsEnumerator())
                {
                    target = prop.GetGenericArgumentType();
                }
                else
                {
                    target = prop.PropertyType;
                }
                if (target != null)
                {
                    DiscoverComplexTypes(target);
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }
    }
}