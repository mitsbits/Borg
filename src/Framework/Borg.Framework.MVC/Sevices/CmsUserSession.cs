using Borg.Framework.Modularity;
using Borg.Infrastructure.Core;
using Borg.Infrastructure.Core.DTO;
using Borg.Infrastructure.Core.Strings.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;

namespace Borg.Framework.MVC.Sevices
{
    [Serializable]
    public class UserSession : Tidings, IUserSession, ICanContextualize
    {
        protected const string SettingsCookieName = "Borg.UserSession"; //TODO: retrieve from settings
        protected const string SessionStartKey = "Borg.SessionStartKey";//TODO: retrieve from settings

        public UserSession(IHttpContextAccessor httpContextAccessor, IJsonConverter jsonConverter)
        {
            HttpContext = Preconditions.NotNull(httpContextAccessor.HttpContext, nameof(httpContextAccessor.HttpContext));
            Serializer = Preconditions.NotNull(jsonConverter, nameof(jsonConverter));
            SessionId = Guid.NewGuid().ToString();
            ReadState();
            SaveState();
        }

        #region IUserSession

        public void StartSession()
        {
            Remove(SessionStartKey);
            SaveState();
        }

        public void StopSession()
        {
            Remove(SessionStartKey);
            HttpContext.Response.Cookies.Delete(SettingsCookieName);
        }

        public DateTimeOffset SessionStart
        {
            get
            {
                if (!ContainsKey(SessionStartKey))
                {
                    var tiding = new Tiding(SessionStartKey);
                    tiding.SetValue(DateTimeOffset.UtcNow, Serializer);
                    Add(tiding);
                    SaveState();
                }
                return RootByKey[SessionStartKey].GetValue<DateTimeOffset>(Serializer);
            }
        }

        public string UserIdentifier => !IsAuthenticated() ? string.Empty : HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        public string UserName => !IsAuthenticated() ? string.Empty : HttpContext.User.Identity.Name;
        private string _displayName;

        private string GetDiplayName()
        {
            if (string.IsNullOrWhiteSpace(_displayName))
            {
                _displayName = !IsAuthenticated()
                    ? string.Empty
                    : HttpContext.User.Claims.Any(x => x.Type == ClaimTypes.Surname) &&
                      HttpContext.User.Claims.Any(x => x.Type == ClaimTypes.GivenName)
                        ? $"{HttpContext.User.Claims.First(x => x.Type == ClaimTypes.GivenName).Value} {HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Surname).Value}"
                        : HttpContext.User.Identity.Name;
            }
            return _displayName;
        }

        public string DisplayName => GetDiplayName();

        private string _avatar;

        private string GetAvatar()
        {
            if (string.IsNullOrWhiteSpace(_avatar))
            {
                _avatar = !IsAuthenticated()
                    ? string.Empty
                    : HttpContext.User.Claims.Any(x => x.Type == BorgClaimTypes.Avatar)
                        ? HttpContext.User.Claims.First(x => x.Type == BorgClaimTypes.Avatar).Value
                        : "";
            }
            return _avatar;
        }

        public string Avatar => GetAvatar();

        public bool IsAuthenticated()
        {
            return HttpContext.User != null && HttpContext.User.Identity.IsAuthenticated;
        }

        public string SessionId { get; }

        public T Setting<T>(string key, T value)
        {
            Preconditions.NotEmpty(key, nameof(key));
            T setting = default(T);
            if (value != null)
            {
                setting = value;
                if (ContainsKey(key))
                {
                    RootByKey[key].SetValue(setting, Serializer);
                }
                else
                {
                    var tiding = new Tiding(key);
                    tiding.SetValue(setting, Serializer);
                    Add(tiding);
                }
                SaveState();
            }
            else
            {
                if (ContainsKey(key))
                {
                    setting = RootByKey[key].GetValue<T>(Serializer);
                }
            }
            return setting;
        }

        public T Setting<T>(string key)
        {
            Preconditions.NotEmpty(key, nameof(key));
            T setting = default(T);
            if (ContainsKey(key))
            {
                setting = RootByKey[key].GetValue<T>(Serializer);
            }
            return setting;
        }

        #endregion IUserSession

        #region ICanContextualize

        public bool ContextAcquired { get; protected set; }

        #endregion ICanContextualize

        [IgnoreDataMember]
        protected virtual HttpContext HttpContext { get; }

        [IgnoreDataMember]
        protected virtual IJsonConverter Serializer { get; }

        protected virtual void SaveState()
        {
            string data = Serializer.Serialize(this as Tidings);
            CookieOptions options = new CookieOptions { HttpOnly = true };
            HttpContext.Response.Cookies.Append(SettingsCookieName, data, options);
        }

        protected virtual void ReadState()
        {
            if (HttpContext.Request.Cookies.ContainsKey(SettingsCookieName))
            {
                var jsonData = HttpContext.Request.Cookies[SettingsCookieName];
                Tidings data = Serializer.DeSerialize<Tidings>(jsonData);
                Clear();
                foreach (var tiding in data)
                {
                    Add(tiding);
                }
            }
        }
    }
}