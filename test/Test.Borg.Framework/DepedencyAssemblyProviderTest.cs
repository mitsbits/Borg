using Borg.Framework.Reflection.Services;
using Borg.Infrastructure.Core.Reflection.Discovery;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Test.Borg.Framework
{
    public class DepedencyAssemblyProviderTest : TestBase
    {
        public DepedencyAssemblyProviderTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void depedency_assembly_providers_loads_assemblies_from_context()
        {
            var provider = new DepedencyAssemblyProvider(_moqLoggerFactory);
            provider.ShouldNotBeNull();
            provider.ShouldBeAssignableTo(typeof(IAssemblyProvider));
            var results = provider.GetAssemblies();
            results.ShouldNotBeEmpty();
        }
    }
}