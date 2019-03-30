
using Borg.Infrastructure.Core.Collections;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Framework.DAL
{
    public interface IReadRepository<T> : IRepository<T> where T : class
    {
        Task<IPagedResult<T>> Find(Expression<Func<T, bool>> predicate, int page, int size,
            IEnumerable<OrderByInfo<T>> orderByy, CancellationToken cancellationToken = default(CancellationToken),
            params Expression<Func<T, dynamic>>[] paths);
    }
}