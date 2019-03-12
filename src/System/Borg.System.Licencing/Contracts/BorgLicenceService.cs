using System;
using System.Collections.Generic;
using System.Text;

namespace Borg.System.Licencing.Contracts
{
    public interface IBorgLicenceService
    {
        BorgLicence Retrieve();
        BorgLicence Install(string pathToLicence);
        int ActiveApplicationServerCount();
        int ActiveApplicationUserCount();
    }
}
