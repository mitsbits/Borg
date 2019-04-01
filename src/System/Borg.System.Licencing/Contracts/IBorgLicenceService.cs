using System;
using System.Collections.Generic;
using System.Text;

namespace Borg.System.Licencing.Contracts
{
    public interface IBorgLicenceService
    {
        IBorgLicence Retrieve();
        int ActiveApplicationServerCount();
        int ActiveApplicationUserCount();
    }



}
