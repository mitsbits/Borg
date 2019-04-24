using Borg.Framework.Dispatch.Contracts;
using Borg.Infrastructure.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Platform.Dispatch.NetCore
{
    public class Dispatcher : IDispatcher
    {
        private readonly ServiceFactory serviceFactory;
        private readonly ILogger logger;

        public Dispatcher(ServiceFactory serviceFactory)
        {
            Preconditions.NotNull(serviceFactory, nameof(serviceFactory));
            this.serviceFactory = serviceFactory;
            var loggerFactory = serviceFactory.GetInstance<ILoggerFactory>();
            logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger(GetType());
        }

        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            var notificationType = notification.GetType();
            var serviceType = typeof(INotificationHandler<>).MakeGenericType(notificationType);
            foreach (var handler in serviceFactory.GetInstances(serviceType))
            {
                var handlerType = handler.GetType();
                if (handlerType.IsSubclassOf(typeof(NotificationHandler)))
                {
                    logger.Trace($"Invoicing motification handler: {handlerType} for notification {notificationType}");
                    ((NotificationHandler)handler).Handle(notification);
                }

                if (handlerType.IsSubclassOf(typeof(AsyncNotificationHandler)))
                {
                    logger.Trace($"Invoicing motification handler: {handlerType} for notification {notificationType}");
                    ((AsyncNotificationHandler)handler).Handle(notification, cancellationToken);
                }
            }
            return Task.CompletedTask;
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<object> Send(object request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> Send<TResponse, TRequest>(TRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}