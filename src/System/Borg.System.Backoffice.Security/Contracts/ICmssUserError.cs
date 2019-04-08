using System;

namespace Borg.System.Backoffice.Security.Contracts
{
    public interface ICmssUserError
    {
        Exception Exception { get; }
        DateTimeOffset EventOn { get; }
    }

    public interface ICmssUserError<out T> : ICmssUserError
    {
        T User { get; }
    }
}