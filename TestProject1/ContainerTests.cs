using Lamar;

using LamarTestground;

namespace TestProject1;

public class ContainerTests
{
    [Fact]
    public void AssertConfigurationIsValid()
    {
        // arrange
        var container = new Container(new LamarRegistry());

        // act && assert
        container.AssertConfigurationIsValid();
    }
}