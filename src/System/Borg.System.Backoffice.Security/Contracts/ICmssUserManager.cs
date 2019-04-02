using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Borg.System.Backoffice.Security.Contracts
{
    public interface ICmsUserManager 
    {
        Task<ICmsUserLoginResult> Login(string user, string password);
        Task<ICmsUserSetPasswordResult> SetPassword(string user, string password);
    }
    public interface ICmsUserPasswordValidator
    {
        Task<(bool isStrong, float score)> IsStrongEnough(string password);
    }
}
