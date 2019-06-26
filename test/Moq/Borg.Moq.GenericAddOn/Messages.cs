using Borg.Framework.Dispatch.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Moq.GenericAddOn
{
    class Topic { }
    class Subscriber : INotificationHandler<Topic>
    {
        public Task Handle(Topic notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
