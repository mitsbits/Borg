using Borg.Framework.Cms;
using System.Threading.Tasks;

namespace Borg.System.Backoffice.Security.Contracts
{
    public interface ICmsUserManager<TData>
    {
        Task<ICmsOperationResult<TData>> Login(string user, string password);

        Task<ICmsOperationResult> SetPassword(string user, string password);
    }

    public interface ICmsRoleManager<TData>
    {
        Task<ICmsOperationResult<TData>> Get(string role);
    }
}