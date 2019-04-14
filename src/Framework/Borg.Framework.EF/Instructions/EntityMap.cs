using Borg.Framework.EF.Instructions.Attributes;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Borg.Platform.EF.Instructions
{
    public interface IEntityMap
    {
        void OnModelCreating(ModelBuilder builder);
    }

    public interface IEntityMap<TEntity, TDbContext> : IEntityMap where TEntity : class where TDbContext : DbContext
    {
    }

    public abstract class EntityMap<TEntity, TDbContext> : IEntityMap<TEntity, TDbContext> where TEntity : class where TDbContext : DbContext
    {
        //private static ConcurrentDictionary<Type, Type> maps = new ConcurrentDictionary<Type, Type>();

        protected EntityMap()
        {
            //maps.AddOrUpdate(GetType().GetGenericArguments()[0], GetType(), (key, oldValue) => GetType());
        }

        public virtual void OnModelCreating(ModelBuilder builder)
        {
            KeySequenceDefinition(builder);
            //ManyToManyDefinition(builder);
            PrimaryKeyDefinition(builder);
            HasManyDefinition(builder);
        }

        private static void HasManyDefinition(ModelBuilder builder)
        {
            var props = typeof(TEntity).GetProperties().Where(x => x.GetCustomAttribute<HasManyDefinitionAttribute>() != null);
            foreach(var prop in props)
            {
                var proptypeinf = prop.PropertyType;
                if (proptypeinf.IsAssignableTo(typeof(ICollection<>))) continue;
                var parentName = typeof(TEntity).Name;
                Type target = proptypeinf.GetType().GetGenericArguments()[0];
                var targetName = target.Name;
                var foreghnKeyColumnName = prop.GetCustomAttribute<HasManyDefinitionAttribute>().ForeighnKeyColumnName;

                var hasManyExpr = $"x=> x.{prop.Name}";
                //Expression<Func<TEntity, IEnumerable<TRelatedEntity>>>
                //builder.Entity<TEntity>().HasMany()
            }


        }

        private static void PrimaryKeyDefinition(ModelBuilder builder)
        {
            var props = typeof(TEntity).GetProperties().Where(x => x.GetCustomAttribute<PrimaryKeyDefinitionAttribute>() != null);
            if (props.Any())
            {
                Expression<Func<TEntity, object>> keyExpression;
                var options = ScriptOptions.Default.AddReferences(typeof(TEntity).Assembly);
                if (props.Count() == 1)
                {
                    var keyExpr = $"x => x.{props.Single().Name}";

                    keyExpression = AsyncHelpers.RunSync(() => CSharpScript.EvaluateAsync<Expression<Func<TEntity, object>>>(keyExpr, options));
                }
                else
                {
                    var sb = new StringBuilder("x => new { ");
                    var keys = props.Select(x => new { x.Name, x.GetCustomAttribute<PrimaryKeyDefinitionAttribute>().Order });
                    sb.Append(string.Join(", ", keys.OrderBy(x => x.Order).ThenBy(x => x.Name).Select(x => $"x.{x.Name}")));
                    sb.Append(" }");

                    var keyExpr = sb.ToString();

                    keyExpression = AsyncHelpers.RunSync(() => CSharpScript.EvaluateAsync<Expression<Func<TEntity, object>>>(keyExpr, options));
                }
                builder.Entity<TEntity>().HasKey(keyExpression).ForSqlServerIsClustered();
            }
        }

        private static void ManyToManyDefinition(ModelBuilder builder)
        {
            var props = typeof(TEntity).GetProperties(BindingFlags.Public);  //TODO: cache this
            foreach (var prop in props)
            {
                var def = prop.GetCustomAttribute<ManyToManyDefinitionAttribute>();
                if (def != null)
                {
                    var proptypeinf = prop.PropertyType;
                    if (proptypeinf.IsAssignableTo(typeof(ICollection<>)))
                    {
                        Type target = proptypeinf.GetType().GetGenericArguments()[0];
                        var names = new string[] { typeof(TEntity).Name, target.Name };
                    }
                    else
                    {
                        throw new ApplicationException("only for collection properties");
                    }
                }
            }
        }

        private static void KeySequenceDefinition(ModelBuilder builder)
        {
            var sqa = typeof(TEntity).GetCustomAttribute<KeySequenceDefinitionAttribute>();
            if (sqa != null)
            {
                var sequenceName = $"{typeof(TEntity).Name}_{sqa.Column}";
                var options = ScriptOptions.Default.AddReferences(typeof(TEntity).Assembly);
                var keyExpr = $"x => x.{sqa.Column}";
                Expression<Func<TEntity, object>> keyExpression = AsyncHelpers.RunSync(() => CSharpScript.EvaluateAsync<Expression<Func<TEntity, object>>>(keyExpr, options));

                builder.HasSequence<int>(sequenceName)
               .StartsAt(1)
               .IncrementsBy(1);

                builder.Entity<TEntity>().HasKey(keyExpression).ForSqlServerIsClustered();
                builder.Entity<TEntity>().Property(keyExpression).HasDefaultValueSql($"NEXT VALUE FOR {sequenceName}");
            }
        }
    }
}