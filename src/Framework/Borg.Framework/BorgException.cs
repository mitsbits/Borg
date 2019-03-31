using System;

namespace Borg.Framework
{
    public abstract class BorgException : Exception
    {
        protected BorgException()
        {
        }

        protected BorgException(string message)
            : base(message)
        {
        }

        protected BorgException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}