using System;

namespace Borg.Framework.Cms
{
    [Serializable]
    public class CmsError : ICmsError<string>
    {
        private readonly Exception _exception;
        private readonly string _user;
        private readonly DateTimeOffset _eventOn;

        public CmsError(string user, Exception exception = default(Exception), DateTimeOffset? eventOn = default(DateTimeOffset?)) : this()
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

        protected CmsError()
        {
            _eventOn = DateTimeOffset.UtcNow;
        }

        public string Data => _user;

        public Exception Exception => _exception;

        public DateTimeOffset EventOn => _eventOn;
    }
}