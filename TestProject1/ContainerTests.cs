using Lamar;
using Lamar.IoC.Instances;

using LamarTestground;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using NSubstitute;

using static Lamar.ServiceRegistry;

namespace TestProject1;

public class ContainerTests
{
    [Fact]
    public void AssertConfigurationIsValid()
    {
        // arrange
        var container = new Container(registry =>
        {
            registry.IncludeRegistry<LamarRegistry>();

            registry.IncludeRegistry<KnownAspNetCoreDependencies>();
        });

        // act && assert
        container.AssertConfigurationIsValid();
    }
}

public class KnownAspNetCoreDependencies : ServiceRegistry
{
    public KnownAspNetCoreDependencies()
    {
        For<IConfiguration>().UseNSubstitute();
        For<IHttpClientFactory>().UseNSubstitute();
        For<IHttpContextAccessor>().UseNSubstitute();
        //For<IOptions<TestOptions>>().UseNSubstitute();

        Policies.OnMissingFamily<MissingIOptionsPolicy>();

        For(typeof(IOptions<>)).Use(new NullInstance(typeof(IOptions<>)));
    }
}

public static class LamarExtensions
{
    public static ObjectInstance UseNSubstitute<TService>(this InstanceExpression<TService> serviceRegistration)
        where TService : class
        => serviceRegistration.Use(Substitute.For<TService>());
}

public class MissingIOptionsPolicy : IFamilyPolicy
{
    public ServiceFamily Build(Type type, ServiceGraph serviceGraph)
    {
        if (type.GetGenericTypeDefinition() != typeof(IOptions<>))
        {
            return null!;
        }

        return new ServiceFamily(type, serviceGraph.DecoratorPolicies,
            ObjectInstance.For(Substitute.For([type], [])));
    }
}