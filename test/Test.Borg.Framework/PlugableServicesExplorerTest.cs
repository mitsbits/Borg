using Borg;
using Borg.Framework.Reflection.Discovery;
using Borg.Framework.Reflection.Services;
using Shouldly;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Test.Borg.Framework
{
    public class PlugableServicesExplorerTest : TestBase
    {
        public PlugableServicesExplorerTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void explorer_returns_results_for_assembly()
        {
            var path = $"{AbsoluteBin}\\Borg.Moq.GenericAddOn.dll";
            var provider = new FileAssemblyProvider(null, path);
            var explorer = new PlugableServicesExplorer(_moqLoggerFactory, new[] { provider });
            var results = explorer.ScanAndResult();
            results.Any().ShouldBeTrue();
        }
    }
}