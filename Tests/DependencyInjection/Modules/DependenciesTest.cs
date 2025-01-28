using Microsoft.Extensions.DependencyInjection;
using SamHowes.Extensions.DependencyInjection.Modules;

namespace SamHowes.Extensions.Tests.DependencyInjection.Modules;

public class DependenciesTest
{
    [Fact]
    public void Dependencies_Work()
    {
        var injectorBuilder = new InjectorBuilder()
            .AddModules(new TopLevel());
        var injector = injectorBuilder.Build();
        injector.Get<TestService>();
    }

    public class TopLevel : TestModule
    {
        public TopLevel()
        {
            Deps<A>();
        }
    }

    public class A : TestModule
    {
        public override void Configure(InjectorBuilder builder)
        {
            builder.Services.AddScoped<TestService>();
        }
    }

    public class TestService;
}

public class TestModule : InjectionModule
{
    public override void Configure(InjectorBuilder builder)
    {
    }
}