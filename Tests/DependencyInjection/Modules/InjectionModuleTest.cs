using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SamHowes.Extensions.DependencyInjection.Modules;

namespace SamHowes.Extensions.Tests.DependencyInjection.Modules;

public class TestService {}

public class TestModule : InjectionModule
{
    public override void Configure(InjectorBuilder builder)
    {
        builder.Add<TestService>();
    }
}

public class InjectionModuleTest
{
    [Fact]
    public void It_Works()
    {
        var injector = MakeInjector();

        var service = injector.Get<TestService>();

        service.Should().NotBeNull();
    }

    private static Injector MakeInjector()
    {
        var injector = new InjectorBuilder()
            .AddModule(new TestModule())
            .Build();
        return injector;
    }

    [Fact]
    public void StartScope_Works()
    {
        var injector = MakeInjector();

        var service = injector.Get<TestService>();
        var secondService = injector.Get<TestService>();

        secondService.Should().Be(service);

        using (var scope = injector.StartScope())
        {
            injector.Provider.Should().Be(scope.Provider);
            injector.Provider.Should().NotBe(injector.RootProvider);
            
            var scopedService = injector.Get<TestService>();
            scopedService.Should().NotBe(service);
            
            var secondScopedService = injector.Get<TestService>();
            secondScopedService.Should().Be(scopedService);
        }
        
        // after scope is disposed
        injector.Provider.Should().Be(injector.RootProvider);
    }
}