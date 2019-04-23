using Borg.Infrastructure.Core.DDD.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Framework.Dispatch.Contracts
{
    public interface INotificationHandler<in TNotification>
    {

        Task Handle(TNotification notification, CancellationToken cancellationToken);
    }

    public abstract class NotificationHandler<TNotification> : INotificationHandler<TNotification>
    {
        Task INotificationHandler<TNotification>.Handle(TNotification notification, CancellationToken cancellationToken)
        {
            Handle(notification);
            return Unit.Task;
        }


        protected abstract void Handle(TNotification notification);
    }
}
