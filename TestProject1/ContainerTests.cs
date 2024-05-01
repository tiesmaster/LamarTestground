using Lamar;
using Lamar.Diagnostics;
using Lamar.IoC.Instances;

using LamarTestground;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

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

    [Fact]
    public async Task VerifyContainerConfiguration_Instances()
    {
        var container = new Container(registry =>
        {
            registry.IncludeRegistry<LamarRegistry>();
        });

        await Verify(container.Model.AllInstances)
            .IgnoreMember<Instance>(x => x.Hash)
            .OrderEnumerableBy<InstanceRef>(x => x.Name);
    }

    [Fact(Skip = "Flaky")]
    public async Task VerifyContainerConfiguration_ServiceTypes()
    {
        var container = new Container(registry =>
        {
            registry.IncludeRegistry<LamarRegistry>();
        });

        await Verify(container.Model.ServiceTypes)
            .IgnoreMember<Instance>(x => x.Hash)
            .OrderEnumerableBy<IServiceFamilyConfiguration>(x => x.ServiceType);
    }

    [Fact]
    public void MyTestMethod()
    {
        //typeof(int).GetGenericTypeDefinition();

        var ti = typeof(int);

        (ti.IsGenericType && ti.GetGenericTypeDefinition() != null).ShouldBeFalse();
    }

    [Fact]
    public void MyTestMethod2()
    {
        typeof(List<int>).GetGenericTypeDefinition();

        typeof(List<int>).IsGenericType.ShouldBeTrue();
        //typeof(List<int>).IsGenericTypeDefinition.ShouldBeTrue();
    }

    [Fact]
    public void MyTestMethod3()
    {
        typeof(List<>).GetGenericTypeDefinition();

        var lo = typeof(List<>);

        lo.IsGenericType.ShouldBeTrue();
        lo.IsGenericTypeDefinition.ShouldBeTrue();

        lo.ShouldBe(lo);

        lo.GetGenericTypeDefinition().ShouldBe(lo);
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