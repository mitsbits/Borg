using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Borg.Platform.EF
{
    public class BorgPlatformDbContextFactory : IDesignTimeDbContextFactory<BorgDb>
    {
        BorgDb IDesignTimeDbContextFactory<BorgDb>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BorgDb>();
            var options = optionsBuilder
                .UseSqlServer("Data Source=.;Initial Catalog=borg;Integrated Security=True;");

            return new BorgDb(optionsBuilder.Options);
        }
    }
}