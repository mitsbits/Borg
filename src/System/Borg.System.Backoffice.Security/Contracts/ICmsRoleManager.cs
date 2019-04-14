using Borg.Framework.Cms.Contracts;
using Borg.Infrastructure.Core.DDD.Contracts;
using System.Threading.Tasks;

namespace Borg.System.Backoffice.Security.Contracts
{
    public interface ICmsRoleManager<TData> where TData : IHasTitle, IEntity<int>
    {
        Task<ICmsOperationResult<TData>> Get(string role);
    }
}