
using Borg.Framework.DAL;
using Borg.Infrastructure.Core.Collections;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Platform.EF.Contracts
{
    public interface IUnitOfWork<out TDbContext> : IUnitOfWork, IHaveDbContext<TDbContext> where TDbContext : DbContext
    {
        IQueryRepository<T> QueryRepo<T>() where T : class;

        IReadWriteRepository<T> ReadWriteRepo<T>() where T : class;
    }

    public interface IUnitOfWork : IDisposable
    {
        Task Save(CancellationToken cancelationToken = default(CancellationToken));
    }
    public interface IQueryRepository<T> : IRepository<T> where T : class
    {
        Task<IPagedResult<T>> Find(Expression<Func<T, bool>> predicate, int page, int records,
            IEnumerable<OrderByInfo<T>> orderBy, CancellationToken cancellationToken = default(CancellationToken),
            params Expression<Func<T, dynamic>>[] paths);
    }

    public interface IReadWriteRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : class
    {
    }
}