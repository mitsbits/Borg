using Borg.Infrastructure.Core.DDD.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Framework.Dispatch.Contracts
{

    public interface IRequestHandler<in TRequest, TResponse>
   
    {
        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }


    public interface IRequestHandler<in TRequest> : IRequestHandler<TRequest, Unit>
    {
    }


    public abstract class AsyncRequestHandler<TRequest> : IRequestHandler<TRequest>
    {
        async Task<Unit> IRequestHandler<TRequest, Unit>.Handle(TRequest request, CancellationToken cancellationToken)
        {
            await Handle(request, cancellationToken).ConfigureAwait(false);
            return Unit.Value;
        }


        protected abstract Task Handle(TRequest request, CancellationToken cancellationToken);
    }

 
    public abstract class RequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    {
        Task<TResponse> IRequestHandler<TRequest, TResponse>.Handle(TRequest request, CancellationToken cancellationToken)
            => Task.FromResult(Handle(request));


        protected abstract TResponse Handle(TRequest request);
    }


    public abstract class RequestHandler<TRequest> : IRequestHandler<TRequest>
    {
        Task<Unit> IRequestHandler<TRequest, Unit>.Handle(TRequest request, CancellationToken cancellationToken)
        {
            Handle(request);
            return Unit.Task;
        }

        protected abstract void Handle(TRequest request);
    }
}
