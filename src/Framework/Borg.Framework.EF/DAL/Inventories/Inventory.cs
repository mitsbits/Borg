using Borg.Framework.DAL;
using Borg.Framework.DAL.Inventories;
using Borg.Framework.EF.Contracts;
using Borg.Infrastructure.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Borg.Framework.EF.DAL.Inventories
{
    public abstract class Inventory<T, TDbContext> : IInventory<T> where T : class where TDbContext : BorgDbContext
    {
        protected readonly ILogger Logger;
        protected readonly IUnitOfWork<TDbContext> UOW;

        protected Inventory(ILoggerFactory loggerFactory, IUnitOfWork<TDbContext> unitOfWork)
        {
            Logger = loggerFactory != null ? loggerFactory.CreateLogger(GetType()) : NullLogger.Instance;
            UOW = Preconditions.NotNull(unitOfWork, nameof(unitOfWork));
        }

        public bool SupportsReads { get { return UOW.GetType().IsAssignableFrom(typeof(IReadWriteUnitOfWork)); } }
        public bool SupportsWrites { get { return UOW.GetType().IsAssignableFrom(typeof(IReadWriteUnitOfWork)); } }
        public bool SupportsQuerable { get { return UOW.GetType().IsAssignableFrom(typeof(IQuerableUnitOfWork)); } }
        public bool SupportsQueries { get { return UOW.GetType().IsAssignableFrom(typeof(IQueryUnitOfWork)); } }
        public bool SupportsProjections { get; protected set; } = false;
        public bool SupportsSearch { get { return UOW.GetType().IsAssignableFrom(typeof(ISearchUnitOfWork)); } }
    }
}