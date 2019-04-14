namespace Borg.Framework.Cms
{
    public interface ICmsOperationResult
    {
        bool Succeded { get; }
        ICmsError[] Errors { get; }
    }

    public interface ICmsOperationResult<out TData> : ICmsOperationResult
    {
        TData Payload { get; }
    }
}