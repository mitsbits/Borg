using Borg.System.Licencing;
using Borg.System.Licencing.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Borg.System.AddOn
{
    public class JsonFileLicenceService : IBorgLicenceService
    {
        private readonly string _path;

        public JsonFileLicenceService(string path)
        {
            _path = path;
    }
        public int ActiveApplicationServerCount()
        {
            throw new NotImplementedException();
        }

        public int ActiveApplicationUserCount()
        {
            throw new NotImplementedException();
        }

        public BorgLicence Install(string pathToLicence)
        {
            throw new NotImplementedException();
        }

        public BorgLicence Retrieve()
        {
            throw new NotImplementedException();
        }

        IBorgLicence IBorgLicenceService.Retrieve()
        {
            throw new NotImplementedException();
        }
    }


}
