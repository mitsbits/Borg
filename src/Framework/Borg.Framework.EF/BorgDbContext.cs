using Borg.Infrastructure.Core.Services.Factory;
using Borg.Platform.EF.Instructions;
using Borg.Platform.EF.Instructions.Attributes;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace Borg.Framework.EF
{
    public abstract class BorgDbContext : DbContext
    {
        protected BorgDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        protected virtual string SchemaName => GetType().Name.Replace("DbContext", string.Empty).Slugify();

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
}