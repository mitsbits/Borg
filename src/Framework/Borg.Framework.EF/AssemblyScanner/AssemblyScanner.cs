using Borg.Framework.Reflection.ObjectGraph;
using Borg.Infrastructure.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Borg.Framework.EF.AssemblyScanner
{
    internal class AssemblyScanner : IDisposable
    {
        private Assembly assembly;
        private List<Type> aggregateRoots;
        private Dictionary<Type, Type[]> complexTypes;
        private readonly ILogger Logger;

        public AssemblyScanner(Assembly assembly, ILogger logger)
        {
            this.assembly = Preconditions.NotNull(assembly, nameof(assembly));
            aggregateRoots = new List<Type>();
            complexTypes = new Dictionary<Type, Type[]>();
            Logger = logger ?? NullLogger.Instance;
            Populate();
        }

        internal AssemblyScanResult Scan()
        {
            return Result;
        }

        private AssemblyScanResult Result;

        private void Populate()
        {
            var aggregates = assembly.GetTypes().Where(t => t.IsCmsAggregateRoot());
            if (!aggregates.Any())
            {
                Result = new AssemblyScanResult(assembly, new string[] { new NoAggregatesException(assembly).ToString() });
                return;
            }

            aggregateRoots.AddRange(aggregates);

            foreach (var aggregate in aggregateRoots)
            {
                using (var recursor = new ComplexTypeRecursor(aggregate, Logger))
                {
                    try
                    {
                        var result = recursor.Results();
                        complexTypes.Add(aggregate, result.Entities());
                    }
                    catch (Exception ex)
                    {
                        Logger.Warn("{0}", ex.Message);
                    }
                }
            }
            Result = new AssemblyScanResult(assembly, aggregateRoots, complexTypes);
        }

        public void Dispose()
        {
            assembly = null;
            aggregateRoots = null;
            complexTypes = null;
        }
    }
}