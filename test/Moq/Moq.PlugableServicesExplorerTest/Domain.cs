using Borg.Infrastructure.Core.DI;

namespace Moq.PlugableServicesExplorerTest
{
    public interface IFoo { }

    public interface IBar { }

    [PlugableService(ImplementationOf = typeof(IFoo))]
    public class Foo { }

    [PlugableService(ImplementationOf = typeof(IBar))]
    public class Bar { }
}