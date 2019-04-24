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
            Preconditions.NotNull(serviceProvider, nameof(serviceProvider));
            this.serviceProvider = serviceProvider;
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger(GetType());
        }

        public T GetInstance<T>()
        {
            logger.Trace($"{nameof(ServiceFactory)} requests service:{typeof(T)} ");
            return serviceProvider.GetRequiredService<T>();
        }

        public IEnumerable<T> GetInstances<T>()
        {
            logger.Trace($"{nameof(ServiceFactory)} requests service:{typeof(T)} ");
            return serviceProvider.GetServices<T>();
        }

        public IEnumerable GetInstances(Type type)
        {
            logger.Trace($"{nameof(ServiceFactory)} requests service:{typeof(T)} ");
            return serviceProvider.GetServices(type);
        }
    }
}