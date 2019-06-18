using Borg;
using Borg.Framework.Reflection.Discovery;
using Borg.Framework.Reflection.Services;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace Test.Borg.Framework
{
    public class PlugableServicesExplorerTest
    {
        [Fact]
        public void explorer_returns_results_for_assembly()
        {
            var directory = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath);
            var dir = new DirectoryInfo(directory);
            var path = $"{dir.FullName}\\Moq.PlugableServicesExplorerTest.dll";
            var provider = new FileAssemblyProvider(null, path);
            var explorer = new PlugableServicesExplorer(null, new[] { provider });
            if (!explorer.ScanCompleted) explorer.Scan();
            var results = explorer.ScanAndResult();
        }
    }
}