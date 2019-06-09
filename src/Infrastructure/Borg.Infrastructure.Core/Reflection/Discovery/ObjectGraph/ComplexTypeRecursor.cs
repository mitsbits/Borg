using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Borg.Infrastructure.Core.Reflection.Discovery.ObjectGraph
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

            DiscoverComplexTypes(root);
        }

        public ComplexTypeRecursorResult Results()
        {
            return Preconditions.NotNull(source, nameof(source));
        }



        private void DiscoverComplexTypes(Type root)
        {
            logger.Trace($"{nameof(ComplexTypeRecursor)} discovering {root.FullName} | Recursion {recursionLevel}");
            source.Add(root, currentReferer, recursionLevel);
            recursionLevel++;
            currentReferer = root;
            foreach (var prop in root.GetProperties())
            {
                if (prop.IsSimple()) continue;
                Type target = null;
                if (prop.IsEnumerator())
                {
                    var collectionType = prop.GetGenericArgumentType();
                    if (collectionType.IsSimple()) continue;
                    target = prop.GetGenericArgumentType();
                }
                else
                {
                    target = prop.PropertyType;
                }
                if (target != null && !source.Any(x => target.Equals(x.Type)))
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