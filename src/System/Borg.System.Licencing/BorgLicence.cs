using System;

namespace Borg.System.Licencing
{
    [Serializable]
    public class BorgLicence
    {
        public string SiteName { get; set; }
        public Guid SiteID { get; set; }
        public int NumberOfServers { get; set; }
        public int NumberOaActiveUsers { get; set; }
    }
}
