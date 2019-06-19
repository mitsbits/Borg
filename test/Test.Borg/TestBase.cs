using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Xunit;
using Xunit.Abstractions;

namespace Test.Borg
{
    public abstract class TestBase
    {
        protected readonly ILoggerFactory _moqLoggerFactory;

        public TestBase(ITestOutputHelper output)
        {
            _moqLoggerFactory = new LoggerFactory();
            _moqLoggerFactory.AddProvider(new XunitLoggerProvider(output));
        }
    }
}