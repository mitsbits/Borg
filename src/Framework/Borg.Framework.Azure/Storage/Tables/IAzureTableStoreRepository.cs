using Borg.Framework.DAL;
using Borg.Infrastructure.Core.DDD.Contracts;
using Borg.Infrastructure.Core.DDD.ValueObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Framework.Azure.Storage.Tables
{
    public interface IAzureTableStoreRepository<T> : IRepository<T> where T : IHaveCompositeKey<string>
    {
        Task<T> Create(T entity, CancellationToken cancellationToken = default(CancellationToken));

        Task<T> Get(CompositeKey<string> key, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<T>> Find(string predicate, CancellationToken cancellationToken = default(CancellationToken));

        Task Delete(CompositeKey<string> key, CancellationToken cancellationToken = default(CancellationToken));
    }
}