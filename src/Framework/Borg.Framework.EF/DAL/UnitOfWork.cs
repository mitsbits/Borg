using Borg.Framework.DAL;
using Borg.Framework.EF.Contracts;
using Borg.Infrastructure.Core;
using Borg.Platform.EF.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Framework.EF.DAL
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> where TDbContext : BorgDbContext
    {
        public UnitOfWork(TDbContext dbContext)
        {
            Context = Preconditions.NotNull(dbContext, nameof(dbContext));
            Context.IsWrappedByUOW = true;
            Context.TrackedEventHandler += OnTracked;
            Context.StateChangedEventHandler += OnStateChanged;
        }

        protected virtual TDbContext Context { get; }

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
            await Save(cancelationToken, false);
        }

        private async Task Save(CancellationToken cancelationToken = default, bool supreseEvents = false)
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
            Context.TrackedEventHandler -= OnTracked;
            Context.StateChangedEventHandler -= OnStateChanged;
            Context.Dispose();
        }

        private void OnTracked(object sender, EntityTrackedEventArgs e)
        {
        }

        private void OnStateChanged(object sender, EntityStateChangedEventArgs e)
        {
        }
    }
}