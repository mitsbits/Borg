using Borg.Framework.Reflection.Services;
using Borg.Infrastructure.Core.Reflection.Discovery;
using Shouldly;
using System;
using System.IO;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace Test.Borg.Framework
{
    public class FileAssemblyProviderTest : TestBase
    {
        private readonly string _moqAssemblyPath;
        private readonly string _notFoundMoqAssemblyPath;

        public FileAssemblyProviderTest(ITestOutputHelper output) : base(output)
        {
            var directory = new DirectoryInfo(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath));
            _moqAssemblyPath = $"{directory.FullName}\\Borg.Moq.GenericAddOn.dll";
            _notFoundMoqAssemblyPath = $"{directory.FullName}\\IDontExist.dll";
        }

        [Fact]
        public void file_assembly_providers_loads_assembly_from_path()
        {
            var provider = new FileAssemblyProvider(_moqLoggerFactory, _moqAssemblyPath);
            provider.ShouldNotBeNull();
            provider.ShouldBeAssignableTo(typeof(IAssemblyProvider));
            var results = provider.GetAssemblies();
            results.ShouldNotBeEmpty();
        }

        [Fact]
        public void file_assembly_providers_should_return_empty_results_from_wrong_path()
        {
            var provider = new FileAssemblyProvider(_moqLoggerFactory, _notFoundMoqAssemblyPath);
            provider.ShouldNotBeNull();
            provider.ShouldBeAssignableTo(typeof(IAssemblyProvider));
            var results = provider.GetAssemblies();
            results.ShouldBeEmpty();
        }
    }
}