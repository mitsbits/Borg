using System.Threading.Tasks;

namespace Borg.Framework.EF.Contracts
{
    public interface IEntityFactory
    {
        Task<TEntity> New<TEntity>() where TEntity : class;
    }
}