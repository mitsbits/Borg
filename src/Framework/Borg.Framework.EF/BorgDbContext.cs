using Borg.Framework.EF.Contracts;
using Borg.Infrastructure.Core.Services.Factory;
using Borg.Platform.EF.Instructions;
using Borg.Platform.EF.Instructions.Attributes;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Framework.EF
{
    public abstract class BorgDbContext : DbContext, IUOWDbContext, IUnitOfWork
    {
        protected BorgDbContext([NotNull] DbContextOptions options, Func<BorgDbContextOptions> borgOptionsFactory = null) : base(options)
        {
            BorgOptions = borgOptionsFactory == null ? new BorgDbContextOptions() : borgOptionsFactory();
        }

        protected BorgDbContext([NotNull] DbContextOptions options, BorgDbContextOptions borgOptions = null) : this(options, () => borgOptions)
        {
        }

        public virtual string Schema => BorgOptions.OverrideSchema.IsNullOrWhiteSpace()
            ? GetType().Name.Replace("DbContext", string.Empty).Slugify()
            : BorgOptions.OverrideSchema.Slugify();

        protected IBorgDbContextOptions BorgOptions { get; }

        public virtual Task Save(CancellationToken cancelationToken = default)
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        break;

                    case EntityState.Deleted:
                        break;

                    case EntityState.Modified:
                        break;

                    default:
                        break;
                }
            }
            return SaveChangesAsync(cancelationToken);
        }

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
                    entityType.Relational().Schema = attr != null ? attr.Schema.Slugify() : Schema;
                }
                else
                {
                    entityType.Relational().Schema = Schema;
                }
            }
        }

        #endregion Private
    }
}