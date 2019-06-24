using Borg.Framework.EF.Discovery;
using Borg.Framework.Reflection.Services;
using Borg.Infrastructure.Core.Reflection.Discovery;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Borg.Platform.EF
{
    public class BorgPlatformDbContextFactory : IDesignTimeDbContextFactory<BorgDb>
    {
        BorgDb IDesignTimeDbContextFactory<BorgDb>.CreateDbContext(string[] args)
        {
            Debugger.Launch();
            var optionsBuilder = new DbContextOptionsBuilder<BorgDb>();
            var options = optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=borg;Integrated Security=True;");

            var directory = new DirectoryInfo(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath)).FullName;
            var provider = new DiskAssemblyProvider(null, directory);
            var explorer = new EntitiesExplorer(null,
                new IAssemblyProvider[]
                {
                new DiskAssemblyProvider(null, directory),
                new DepedencyAssemblyProvider(null),
                new ReferenceAssemblyProvider(null, null, new Assembly[] {Assembly.GetExecutingAssembly()
                })
        });
            var result = new AssemblyExplorerResult(null, new IAssemblyExplorer[] { explorer });
            return new BorgDb(optionsBuilder.Options, result);
        }
    }
}