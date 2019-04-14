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

    public interface ICmsUserLoginResult<out TData> : ICmsUserOperationResult<TData>
    {
    }

    public interface ICmsUserSetPasswordResult : ICmsUserOperationResult
    {
    }
}