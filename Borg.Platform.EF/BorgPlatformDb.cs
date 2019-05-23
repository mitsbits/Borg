using Borg.Framework.EF;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Borg.Platform.EF
{
    public class BorgPlatformDb : BorgDbContext<IConfiguration>
    {
        public BorgPlatformDb(ILoggerFactory loggerFactory, IConfiguration configuration) : base(loggerFactory,configuration)
        {
        }
    }
}
