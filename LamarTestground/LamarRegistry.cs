using Lamar;

namespace LamarTestground;

public class LamarRegistry : ServiceRegistry
{
    public LamarRegistry()
    {
        For<ITestClass>().Use<TestClass>();

        For<Foo>().Use<Foo>();
    }
}
