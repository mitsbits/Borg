using Borg.Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Borg.Platform.Dispatch.NetCore
{
    public class ServiceFactory
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger logger;

        public ServiceFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = Preconditions.NotNull(serviceProvider, nameof(serviceProvider));
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger(GetType());
        }

        public T GetInstance<T>()
        {
            using (var scope = serviceProvider.CreateScope())
            {
                logger.Trace($"{nameof(ServiceFactory)} requests service:{typeof(T)} ");
                return scope.ServiceProvider.GetRequiredService<T>();
            }
        }

        public object GetInstance(Type type)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                logger.Trace($"{nameof(ServiceFactory)} requests service:{type} ");
                return scope.ServiceProvider.GetRequiredService(type);
            }
        }

        public IEnumerable<T> GetInstances<T>()
        {
            using (var scope = serviceProvider.CreateScope())
            {
                logger.Trace($"{nameof(ServiceFactory)} requests service:{typeof(T)} ");
                return scope.ServiceProvider.GetServices<T>();
            }
        }

        public IEnumerable GetInstances(Type type)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                logger.Trace($"{nameof(ServiceFactory)} requests service:{type} ");
                return scope.ServiceProvider.GetServices(type);
            }
        }
    }
}