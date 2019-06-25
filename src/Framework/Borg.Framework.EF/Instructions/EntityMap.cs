using Borg.Framework.EF.Instructions;
using Microsoft.EntityFrameworkCore;

namespace Borg.Platform.EF.Instructions
{
    public class EntityMap<TEntity, TDbContext> : EntityMapBase<TEntity, TDbContext> where TEntity : class where TDbContext : DbContext
    {
    }
}