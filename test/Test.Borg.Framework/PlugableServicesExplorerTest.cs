using Borg;
using Borg.Framework.Reflection.Discovery;
using Borg.Framework.Reflection.Services;
using System.IO;
using Xunit;

namespace Test.Borg.Framework
{
    public class PlugableServicesExplorerTest
    {
        [Fact]
        public void explorer_returns_results_for_assembly()
        {
            var directory = Path.GetDirectoryName(GetType().Assembly.CodeBase);
            var path = $"{directory}\\Moq.PlugableServicesExplorerTest.dll";
            var provider = new FileAssemblyProvider(null, path);
            var explorer = new PlugableServicesExplorer(null, new[] { provider });
            if (!explorer.ScanCompleted) explorer.Scan();
            var results = explorer.ScanAndResult();
        }
    }
}