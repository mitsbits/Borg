using System;

namespace Borg.System.Backoffice.Security.Contracts
{
    [Serializable]
    public class CmssUserError : ICmssUserError<string>
    {
        private readonly Exception _exception;
        private readonly string _user;
        private readonly DateTimeOffset _eventOn;

        public CmssUserError(string user, Exception exception = default(Exception), DateTimeOffset? eventOn = default(DateTimeOffset?)) : this()
        {
            _user = user;
            if (eventOn.HasValue)
            {
                _eventOn = eventOn.Value.ToUniversalTime();
            }
            if (exception != default(Exception))
            {
                _exception = exception;
            }
        }

        protected CmssUserError()
        {
            _eventOn = DateTimeOffset.UtcNow;
        }

        public string User => _user;

        public Exception Exception => _exception;

        public DateTimeOffset EventOn => _eventOn;
    }
}