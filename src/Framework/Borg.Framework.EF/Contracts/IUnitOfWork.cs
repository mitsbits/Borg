using Borg.Framework.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Framework.EF.Contracts
{
    public interface IUnitOfWork<out TDbContext> : IUnitOfWork, IEntityFactory where TDbContext : DbContext
    {
        IQueryRepository<T> QueryRepo<T>() where T : class;

        IReadWriteRepository<T> ReadWriteRepo<T>() where T : class;
    }

    public interface IUnitOfWork : IDisposable
    {
        Task Save(CancellationToken cancelationToken = default(CancellationToken));
    }
}