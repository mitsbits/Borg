using Borg.Infrastructure.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;

namespace Borg.Framework.Reflection.ObjectGraph
{
    public class ComplexTypeRecursor : IDisposable
    {
        private readonly ComplexTypeRecursorResult source;
        private readonly ILogger logger;
        private int recursionLevel;
        private Type currentReferer;

        public ComplexTypeRecursor(Type root, ILogger logger = null)
        {
            source = new ComplexTypeRecursorResult();
            this.logger = logger ?? NullLogger.Instance;
            recursionLevel = 0;
            DiscoverComplexTypes(Preconditions.NotNull(root, nameof(root)));
        }

        public ComplexTypeRecursorResult Results()
        {
            return Preconditions.NotNull(source, nameof(source));
        }

        private void DiscoverComplexTypes(Type root)
        {
            logger.Debug($"{nameof(ComplexTypeRecursor)} discovering {root.FullName} | Recursion {recursionLevel}");
            if (!source.TryAdd(root, currentReferer, recursionLevel)) return;
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