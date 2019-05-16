using System.Threading.Tasks;

namespace Borg.Framework.EF.Contracts
{
    public interface IEntityFactory
    {
        Task<TEntity> NewInstance<TEntity>() where TEntity : class;
    }
}