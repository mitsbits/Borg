using Borg.Framework.Reflection.Services;
using Borg.Infrastructure.Core.Reflection.Discovery;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Test.Borg.Framework
{
    public class DiskAssemblyProviderTest : TestBase
    {
        private readonly string _path;

        public DiskAssemblyProviderTest(ITestOutputHelper output) : base(output)
        {
            _path = AbsoluteBin;
        }

        [Fact]
        public void disk_assembly_providers_loads_assemblies_from_folder()
        {
            var provider = new DiskAssemblyProvider(_moqLoggerFactory, _path);
            provider.ShouldNotBeNull();
            provider.ShouldBeAssignableTo(typeof(IAssemblyProvider));
            var results = provider.GetAssemblies();
            results.ShouldNotBeEmpty();
        }
    }
}