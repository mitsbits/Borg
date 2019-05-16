using Borg.Framework.DAL;
using Borg.Framework.EF.Contracts;
using Borg.Platform.EF.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Framework.EF.DAL
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        private readonly TDbContext _context;

        public UnitOfWork(TDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected virtual TDbContext Context => _context;

        public IQueryRepository<T> QueryRepo<T>() where T : class
        {
            return Context.QueryRepo<T, TDbContext>();
        }

        public IReadWriteRepository<T> ReadWriteRepo<T>() where T : class
        {
            return Context.ReadWriteRepo<T, TDbContext>();
        }

        public async Task Save(CancellationToken cancelationToken = default)
        {
            try
            {
                await Context.SaveChangesAsync(cancelationToken);
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new ConcurrentModificationException(
                    "The record you attempted to edit was modified by another " +
                    "user after you loaded it. The edit operation was cancelled and the " +
                    "currect values in the database are displayed. Please try again.", exception);
            }
        }

        public Task<TEntity> NewInstance<TEntity>() where TEntity : class
        {
            if (Context.EntityIsMapped<TEntity, TDbContext>()) throw new EntityNotMappedException<TDbContext>(typeof(TEntity));
            return Task.FromResult(Infrastructure.Core.Services.Factory.New<TEntity>.Instance.Invoke()); //TODO: this is not a nice namespace, move it to Borg
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}