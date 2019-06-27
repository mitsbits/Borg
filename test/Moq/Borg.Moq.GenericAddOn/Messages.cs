using Borg.Framework.Dispatch.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Moq.GenericAddOn
{
    internal class Topic
    { }

    internal class Subscriber : INotificationHandler<Topic>
    {
        public Task Handle(Topic notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}