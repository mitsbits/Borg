using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Borg.Platform.EF
{
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