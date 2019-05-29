using Borg.Framework.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Borg.Platform.EF
{
    public class BorgPlatformDb : BorgDbContext<IConfiguration>
    {
        public BorgPlatformDb(ILoggerFactory loggerFactory, IConfiguration configuration) : base(loggerFactory, configuration)
        {
        }

        internal BorgPlatformDb(DbContextOptions options) : base(options)
        {
        }
    }

    public class BorgPlatformDbContextFactory : IDesignTimeDbContextFactory<BorgPlatformDb>
    {
        BorgPlatformDb IDesignTimeDbContextFactory<BorgPlatformDb>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BorgPlatformDb>();
            var options = optionsBuilder
                .UseSqlServer("Data Source=.;Initial Catalog=borg;Integrated Security=True;");

            return new BorgPlatformDb(optionsBuilder.Options);
        }
    }
}