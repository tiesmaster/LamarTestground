using Lamar;

namespace LamarTestground;

public class LamarRegistry : ServiceRegistry
{
    public LamarRegistry()
    {
        For<ITestClass>().Use<TestClass>(); //.Ctor<Foo>().Named("a");

        For<Foo>().Use<Foo>(); //.Named("a");
    }
}
