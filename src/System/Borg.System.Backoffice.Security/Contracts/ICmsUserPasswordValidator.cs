using System.Threading.Tasks;

namespace Borg.System.Backoffice.Security.Contracts
{
    public interface ICmsUserPasswordValidator
    {
        Task<(bool isStrong, float score)> IsStrongEnough(string password);
    }
}