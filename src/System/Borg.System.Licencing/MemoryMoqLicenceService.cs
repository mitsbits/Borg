using Borg.System.Licencing.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Borg.System.Licencing
{
    public class MemoryMoqLicenceService : IBorgLicenceService
    {
        private static readonly MemoryMoqLicence moqLicence = new MemoryMoqLicence();
        public int ActiveApplicationServerCount()
        {
            return moqLicence.ActiveApplicationServerCount();
        }

        public int ActiveApplicationUserCount()
        {
            return moqLicence.ActiveApplicationUserCount();
        }

        public IBorgLicence Retrieve()
        {
            return moqLicence;
        }
    }
    public class MemoryMoqLicence: IBorgLicence
    {

        private static Guid _SiteID = Guid.NewGuid();

        public DateTimeOffset? Expires => default(DateTimeOffset?);

        public string SiteName { get => "Borg"; set => throw new NotImplementedException(); }
        public Guid SiteID { get => _SiteID; set => throw new NotImplementedException(); }

        public int ActiveApplicationServerCount()
        {
            return 3;
        }

        public int ActiveApplicationUserCount()
        {
            return 3;
        }
    }
}
