using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SamHowes.Extensions.DependencyInjection.Modules;

namespace SamHowes.Extensions.Tests.DependencyInjection.Modules;

public class TestService {}

public class TestModule : InjectionModule
{
    public override void Configure(InjectorBuilder builder)
    {
        builder.Services.AddScoped<TestService>();
    }
}

public class InjectionModuleTest
{
    [Fact]
    public void It_Works()
    {
        var injector = new InjectorBuilder()
            .AddModule(new TestModule())
            .Build();

        var service = injector.Get<TestService>();

        service.Should().NotBeNull();
    }
}