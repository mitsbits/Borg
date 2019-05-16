using System;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Framework.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IQueryRepository<T> QueryRepo<T>() where T : class;

        IReadWriteRepository<T> ReadWriteRepo<T>() where T : class;

        Task Save(CancellationToken cancelationToken = default);
    }
}