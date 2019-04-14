using Borg.Platform.EF.Instructions.Attributes;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Reflection;

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
        public virtual void OnModelCreating(ModelBuilder builder)
        {
            var sqa = typeof(TEntity).GetCustomAttribute<KeySequenceDefinitionAttribute>();
            if(sqa != null)
            {
                var sequenceName = $"{typeof(TEntity).Name}_{sqa.Column}";
                var options = ScriptOptions.Default.AddReferences(typeof(TEntity).Assembly);
                var keyExpr = $"x => x.{sqa.Column}";
                Expression<Func<TEntity, object>> keyExpression = AsyncHelpers.RunSync(()=> CSharpScript.EvaluateAsync<Expression<Func<TEntity, object>>>(keyExpr, options));
        
                builder.HasSequence<int>(sequenceName)
               .StartsAt(1)
               .IncrementsBy(1);
                
                builder.Entity<TEntity>().HasKey(keyExpression).ForSqlServerIsClustered();
                builder.Entity<TEntity>().Property(keyExpression).HasDefaultValueSql($"NEXT VALUE FOR {sequenceName}");
            }
        }
    }
}