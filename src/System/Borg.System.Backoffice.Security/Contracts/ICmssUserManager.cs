using System.Threading.Tasks;

namespace Borg.System.Backoffice.Security.Contracts
{
    public interface ICmsUserManager<TData>
    {
        Task<ICmsUserLoginResult<TData>> Login(string user, string password);

        Task<ICmsUserSetPasswordResult> SetPassword(string user, string password);
    }
}