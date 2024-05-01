using Microsoft.Extensions.Options;

namespace LamarTestground;

public class TestClass : ITestClass
{
    public TestClass(IOptions<TestOptions> options)
    {
        Options = options.Value;
    }

    public TestOptions Options { get; }

    public string Greeting => Options.MyProperty;
}