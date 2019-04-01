using System;
using System.Collections.Generic;
using System.Text;

namespace Borg.System.Licencing.Contracts
{
    public interface IBorgLicence
    {
        DateTimeOffset? Expires { get; }
        int ActiveApplicationServerCount();
        int ActiveApplicationUserCount();
        string SiteName { get; set; }
        Guid SiteID { get; set; }
    }


}
