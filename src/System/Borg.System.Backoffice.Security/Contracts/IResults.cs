using Borg.Framework.DAL;
using Borg.System.Backoffice.Security.Domain;

namespace Borg.System.Backoffice.Security.Contracts
{
    public interface ICmsUserOperationResult
    {
        bool Succeded { get; }
        ICmssUserError[] Errors { get; }
    }

    public interface ICmsUserOperationResult<out TData> : ICmsUserOperationResult
    {
        TData Payload { get; }
    }

    public interface ICmsUserLoginResult : ICmsUserOperationResult<User>
    {
    }

    public interface ICmsUserSetPasswordResult : ICmsUserOperationResult
    {
    }


}