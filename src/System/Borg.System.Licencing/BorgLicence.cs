using Borg.System.Licencing.Contracts;
using System;

namespace Borg.System.Licencing
{
    [Serializable]
    public class BorgLicence : IBorgLicence
    {
        public DateTimeOffset? Expires => Expiration;

        public DateTimeOffset? Expiration { get; set; }
        public string SiteName { get; set; }
        public Guid SiteID { get; set; }

        public int ActiveApplicationServers { get; set; }

        public int ActiveApplicationUsers { get; set; }

        public int ActiveApplicationServerCount()
        {
            return ActiveApplicationServers;
        }

        public int ActiveApplicationUserCount()
        {
            return ActiveApplicationUsers;
        }
    }
}