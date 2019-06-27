using Borg.Framework.EF.Instructions.Attributes.Schema;
using Borg.Infrastructure.Core;
using Borg.Infrastructure.Core.Exceptions;
using Borg.Platform.EF.Instructions.Contracts;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Borg.Framework.EF.Instructions
{
    public abstract partial class GenericEntityMap<TEntity, TDbContext> : EntityMapBase, IEntityMap<TEntity, TDbContext> where TEntity : class where TDbContext : DbContext
    {
        protected GenericEntityMap() : base(typeof(TEntity), typeof(TDbContext))
        {
        }
        #region OnModelCreating
        public override void OnModelCreating(ModelBuilder builder)
        {
            SequenceDefinition(builder);
            IndexDefinition(builder);

            //HasManyDefinition(builder);
        }

        private void SequenceDefinition(ModelBuilder builder)
        {
            var sqa = EntityType.GetCustomAttribute<SequenceDefinitionAttribute>();
            if (sqa != null)
            {
                var sequenceName = $"{typeof(TEntity).Name}_{sqa.Column}_seq";
                var options = ScriptOptions.Default.AddReferences(EntityType.Assembly);
                var keyExpr = $"x => x.{sqa.Column}";
                var keyExpression = AsyncHelpers.RunSync(() => CSharpScript.EvaluateAsync<Expression<Func<TEntity, object>>>(keyExpr, options));

                builder.HasSequence<int>(sequenceName)
               .StartsAt(sqa.StartsAt)
               .IncrementsBy(sqa.IncrementsBy);

                builder.Entity<TEntity>().Property(keyExpression).HasDefaultValueSql($"NEXT VALUE FOR {sequenceName}");

                var ixsqa = sqa as IndexSequenceDefinitionAttribute;
                if (ixsqa != null)
                {
                    var pksqa = ixsqa as PrimaryKeySequenceDefinitionAttribute;
                    if (pksqa != null)
                    {
                        builder.Entity<TEntity>().HasKey(keyExpression).HasName($"PK_{sqa.Column}").ForSqlServerIsClustered();
                    }
                    else
                    {
                        builder.Entity<TEntity>().HasIndex(keyExpression).HasName($"IX_{sqa.Column}");
                    }
                }
            }

            
        }

        private void IndexDefinition(ModelBuilder builder)
        {
            var props = EntityType.GetProperties().Where(x => x.GetCustomAttribute<IndexDefinitionAttribute>() != null);
            if (!props.Any()) return;
            var options = ScriptOptions.Default.AddReferences(EntityType.Assembly);
            var dataset = props.Select(x => new { prop = x, attr = x.GetCustomAttribute<IndexDefinitionAttribute>() });
            var groups = dataset.GroupBy(x => x.attr.IndexName);

            string IndexName(string indexName, IndexDefinitionAttribute.IndexDefinitionMode mode, IEnumerable<PropertyInfo> properties)
            {
                if (indexName != IndexDefinitionAttribute.DefaultIndexName) return indexName;

                var propertypath = string.Join("_", properties.Select(x => x.Name));
                var prefix = string.Empty;
                switch (mode)
                {
                    case IndexDefinitionAttribute.IndexDefinitionMode.UniqueIndex:
                        prefix = "UX";
                        break;

                    case IndexDefinitionAttribute.IndexDefinitionMode.PrimaryKey:
                        prefix = "PK";
                        break;

                    default:
                        prefix = "IX";
                        break;
                }
                return $"{prefix}_{propertypath}";
            }

            foreach (var @group in groups)
            {
                var indexName = group.Key;
                if (@group.Count() == 1)
                {
                    var prop = props.First();
                    var keyExpr = $"x => x.{prop.Name}";
                    Expression<Func<TEntity, object>> keyExpression = AsyncHelpers.RunSync(() => CSharpScript.EvaluateAsync<Expression<Func<TEntity, object>>>(keyExpr, options));

                    if (prop.GetCustomAttribute<UniqueIndexDefinitionAttribute>() != null)
                    {
                        if (prop.GetCustomAttribute<PrimaryKeyDefinitionAttribute>() != null)
                        {
                            indexName = IndexName(indexName, IndexDefinitionAttribute.IndexDefinitionMode.PrimaryKey, group.Select(x => x.prop));
                            builder.Entity<TEntity>().HasKey(keyExpression).HasName(indexName).ForSqlServerIsClustered();
                        }
                        else
                        {
                            indexName = IndexName(indexName, IndexDefinitionAttribute.IndexDefinitionMode.UniqueIndex, group.Select(x => x.prop));
                            builder.Entity<TEntity>().HasIndex(keyExpression).IsUnique().HasName(indexName);
                        }
                    }
                    else
                    {
                        indexName = IndexName(indexName, IndexDefinitionAttribute.IndexDefinitionMode.Index, group.Select(x => x.prop));
                        builder.Entity<TEntity>().HasIndex(keyExpression).HasName(indexName);
                    }
                }
                else
                {
                    var sb = new StringBuilder("x => new { ");
                    sb.Append(string.Join(", ", @group.OrderBy(x => x.attr.Order).Select(x => $"x.{x.prop.Name}")));
                    sb.Append(" }");
                    var keyExpr = sb.ToString();
                    Expression<Func<TEntity, object>> keyExpression = AsyncHelpers.RunSync(() => CSharpScript.EvaluateAsync<Expression<Func<TEntity, object>>>(keyExpr, options));
                    if (@group.All(x => x.prop.GetCustomAttribute<UniqueIndexDefinitionAttribute>() != null))
                    {
                        if (@group.All(x => x.prop.GetCustomAttribute<PrimaryKeyDefinitionAttribute>() != null))
                        {
                            indexName = IndexName(indexName, IndexDefinitionAttribute.IndexDefinitionMode.PrimaryKey, group.Select(x => x.prop));
                            builder.Entity<TEntity>().HasKey(keyExpression).HasName(indexName).ForSqlServerIsClustered();
                        }
                        else
                        {
                            indexName = IndexName(indexName, IndexDefinitionAttribute.IndexDefinitionMode.UniqueIndex, group.Select(x => x.prop));
                            builder.Entity<TEntity>().HasIndex(keyExpression).IsUnique().HasName(indexName);
                        }
                    }
                    else
                    {
                        indexName = IndexName(indexName, IndexDefinitionAttribute.IndexDefinitionMode.Index, group.Select(x => x.prop));
                        builder.Entity<TEntity>().HasIndex(keyExpression).HasName(@group.Key);
                    }
                }
            }
        }

        //private static void HasManyDefinition(ModelBuilder builder)
        //{
        //    var props = typeof(TEntity).GetProperties().Where(x => x.GetCustomAttribute<HasManyDefinitionAttribute>() != null);
        //    foreach (var prop in props)
        //    {
        //        var proptypeinf = prop.PropertyType;
        //        if (proptypeinf.IsAssignableTo(typeof(ICollection<>))) continue;
        //        var parentName = typeof(TEntity).Name;
        //        Type target = proptypeinf.GetType().GetGenericArguments()[0];
        //        var targetName = target.Name;
        //        var foreghnKeyColumnName = prop.GetCustomAttribute<HasManyDefinitionAttribute>().ForeighnKeyColumnName;

        //        var hasManyExpr = $"x=> x.{prop.Name}";
        //        //Expression<Func<TEntity, IEnumerable<TRelatedEntity>>>
        //        //builder.Entity<TEntity>().HasMany()
        //    }
        //}

        //private static void ManyToManyDefinition(ModelBuilder builder)
        //{
        //    var props = typeof(TEntity).GetProperties(BindingFlags.Public);  //TODO: cache this
        //    foreach (var prop in props)
        //    {
        //        var def = prop.GetCustomAttribute<ManyToManyDefinitionAttribute>();
        //        if (def != null)
        //        {
        //            var proptypeinf = prop.PropertyType;
        //            if (proptypeinf.IsAssignableTo(typeof(ICollection<>)))
        //            {
        //                Type target = proptypeinf.GetType().GetGenericArguments()[0];
        //                var names = new string[] { typeof(TEntity).Name, target.Name };
        //            }
        //            else
        //            {
        //                throw new ApplicationException("only for collection properties");
        //            }
        //        }
        //    }
        //} 
        #endregion
    }

    public abstract class EntityMapBase : IEntityMap
    {
        protected EntityMapBase(Type entityType, Type contextType)
        {
            EntityType = Preconditions.NotNull(entityType, nameof(entityType));
            contextType = Preconditions.NotNull(contextType, nameof(contextType));
            if (!contextType.IsSubclassOf(typeof(DbContext)))
            {
                throw new NotSubclassOfException(contextType, typeof(DbContext));
            }
            ContextType = contextType;
        }

        public Type EntityType { get; }

        public Type ContextType { get; }

        public abstract void OnModelCreating(ModelBuilder builder);
    }
}