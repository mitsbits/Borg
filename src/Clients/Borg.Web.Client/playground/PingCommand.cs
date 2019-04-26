using Borg.Framework.Dispatch.Contracts;
using Borg.Infrastructure.Core.DI;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Web.Client.playground
{
    public class PingCommand
    {
        public string Data { get; set; } = "Ping";
    }

    public class PingBlindCommand
    {
        public string Data { get; set; } = "Ping";
    }

    public class PingQuery
    {
        public string Data { get; set; } = "Ping";
    }

    public class PingNotification
    {
        public string Data { get; set; } = "Ping";
    }

    [PlugableService(ImplementationOf = typeof(IRequestHandler<PingCommand, PongResponse>), Lifetime = Lifetime.Scoped, OneOfMany = true, Order = 1)]
    public class PingCommandHandler : AsyncRequestHandler<PingCommand, PongResponse>
    {
        public override Task<object> Handle(object request, CancellationToken cancellationToken)
        {
            var result = new PongResponse();
            result.Data += ((PingCommand)request).Data;
            return Task.FromResult(result as object);
        }
    }

    [PlugableService(ImplementationOf = typeof(IRequestHandler<PingBlindCommand>), Lifetime = Lifetime.Scoped, OneOfMany = true, Order = 1)]
    public class PingBlindCommandHandler : AsyncRequestHandler<PingBlindCommand>
    {
        public override Task<object> Handle(object request, CancellationToken cancellationToken)
        {
            var result = new PongResponse();
            result.Data += ((PingBlindCommand)request).Data;
            return Task.FromResult(result as object);
        }
    }

    [PlugableService(ImplementationOf = typeof(IRequestHandler<PingQuery, PongResponse>), Lifetime = Lifetime.Scoped, OneOfMany = true, Order = 1)]
    public class PingQueryHandler : AsyncRequestHandler<PingQuery, PongResponse>
    {
        public override Task<object> Handle(object request, CancellationToken cancellationToken)
        {
            var result = new PongResponse();
            result.Data += ((PingCommand)request).Data;
            return Task.FromResult(result as object);
        }
    }

    [PlugableService(ImplementationOf = typeof(INotificationHandler<PingNotification>), Lifetime = Lifetime.Scoped, OneOfMany = true, Order = 1)]
    public class PingNotificationHandler : NotificationHandler<PingNotification>
    {
        public override void Handle(object notification)
        {
            var data = ((PingNotification)notification).Data;
        }
    }

    public class PongResponse
    {
        public string Data { get; set; } = "Pong";
    }
}