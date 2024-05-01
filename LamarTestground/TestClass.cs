using Microsoft.Extensions.Options;

namespace LamarTestground;

public class TestClass
{
    public TestClass(IOptions<TestOptions> options)
    {
        Options = options.Value;
    }

    public TestOptions Options { get; }
}