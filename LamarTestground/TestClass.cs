using Microsoft.Extensions.Options;

namespace LamarTestground;

public class TestClass : ITestClass
{
    public TestClass(IOptions<TestOptions> options, Foo dep)
    {
        Options = options.Value;
    }

    public TestOptions Options { get; }

    public string Greeting => Options.MyProperty;
}

public class Foo
{
}