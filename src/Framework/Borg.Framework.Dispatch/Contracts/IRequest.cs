using Borg.Infrastructure.Core.DDD.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Borg.Framework.Dispatch.Contracts
{
    public interface IRequest : IRequest<Unit> { }


    public interface IRequest<out TResponse> : IBaseRequest { }


    public interface IBaseRequest { }
}
