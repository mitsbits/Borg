using System;

namespace Borg.Framework.Cms
{
    public interface ICmsError
    {
        Exception Exception { get; }
        DateTimeOffset EventOn { get; }
    }

    public interface ICmsError<out T> : ICmsError
    {
        T Data { get; }
    }
}