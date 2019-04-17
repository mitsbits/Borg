using Borg.Framework.EF.Contracts;
using Borg.Infrastructure.Core;
using Borg.Infrastructure.Core.Services.Factory;
using Borg.Platform.EF.Instructions;
using Borg.Platform.EF.Instructions.Attributes;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Framework.EF
{
    public abstract class BorgDbContext : DbContext, IUOWDbContext
    {
        protected BorgDbContext([NotNull] DbContextOptions options, Func<IBorgDbContextOptions> borgOptionsFactory = null) : base(options)
        {
            BorgOptions = borgOptionsFactory == null ? new BorgDbContextOptions() : borgOptionsFactory();
        }

        protected BorgDbContext([NotNull] DbContextOptions options, IBorgDbContextOptions borgOptions = null) : base(options)
        {
            BorgOptions = borgOptions == null ? new BorgDbContextOptions() : borgOptions;
        }

        public virtual string Schema => BorgOptions.OverrideSchema.IsNullOrWhiteSpace()
            ? GetType().Name.Replace("DbContext", string.Empty).Slugify() 
            : BorgOptions.OverrideSchema.Slugify();

        protected IBorgDbContextOptions BorgOptions { get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            Map(builder);
            TableSchema(builder);
        }

        #region Private
        private void Map(ModelBuilder builder)
        {
            var maptype = typeof(EntityMap<,>);
            var maps = GetType().Assembly.GetTypes().Where(t => t.IsSubclassOfRawGeneric(maptype) && !t.IsAbstract && t.BaseType.GenericTypeArguments[1] == GetType());
            foreach (var map in maps)
            {
                ((IEntityMap)New.Creator(map)).OnModelCreating(builder);
            }


        }

        private void TableSchema(ModelBuilder builder)
        {
            foreach (var (entityType, t) in from entityType in builder.Model.GetEntityTypes()
                                            let t = entityType.ClrType
                                            select (entityType, t))
            {
                if (t != null)
                {
                    var attr = t.GetCustomAttribute<TableSchemaDefinitionAttribute>();
                    entityType.Relational().Schema = attr != null ? attr.Schema.Slugify() : SchemaName;
                }
                else
                {
                    entityType.Relational().Schema = SchemaName;
                }
            }
        }
        #endregion
    }

    public abstract class DbSeed<TDbContext> : IDbSeed where TDbContext : DbContext
    {
        protected ILogger Logger { get; }
        protected TDbContext DB { get; }

        protected DbSeed(TDbContext dbContext, ILoggerFactory loggerFactory = null)
        {
            Preconditions.NotNull(dbContext, nameof(dbContext));
            DB = dbContext;
            Logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger(GetType());
        }

        public virtual int Order { get; set; }

        public virtual async Task Run(CancellationToken cancelationToken = default(CancellationToken))
        {
            Logger.Debug($"{nameof(IDbSeed)}:{GetType().Name} is about to run");
            await DB.Database.MigrateAsync(cancelationToken);
            Logger.Debug($"{nameof(IDbSeed)}:{GetType().Name} run");
        }
    }
}