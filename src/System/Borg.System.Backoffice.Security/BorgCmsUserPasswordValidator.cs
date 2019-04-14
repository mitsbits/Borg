using Borg.System.Backoffice.Security.Contracts;
using System.Threading.Tasks;

namespace Borg.System.Backoffice.Security
{
    public class BorgCmsUserPasswordValidator : ICmsUserPasswordValidator
    {
        public Task<(bool isStrong, float score)> IsStrongEnough(string password)
        {
            return Task.FromResult((isStrong: password.Length > 3, score: 0.8f));
        }
    }
}