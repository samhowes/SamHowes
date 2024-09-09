using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SamHowes.Extensions.DependencyInjection.Modules;

namespace SamHowes.Extensions.Tests.DependencyInjection.Modules;

public class TestService {}

public class TestConfiguration
{
    public int? ConfigurationProperty { get; set; }
}

public class TestModule : InjectionModule
{
    public override IEnumerable<string> ConfigurationFiles { get; } = new[] {"Configuration.yaml"};
    public TestConfiguration? Config { get; set; }
    
    public override void Configure(InjectorBuilder builder)
    {
        builder.Add<TestService>();
        Config = builder.Configuration.GetSection("test").Get<TestConfiguration>();
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

    [Fact]
    public void ConfigurationFiles_AreLoadedBeforeConfigure()
    {
        var module = new TestModule();
        module.Config.Should().BeNull();
        new InjectorBuilder()
            .AddModule(module)
            .Build();
        
        module.Config.Should().NotBeNull();
        module.Config!.ConfigurationProperty.Should().NotBeNull();
        module.Config!.ConfigurationProperty.Should().Be(5);
    }

    [Fact]
    public void ConfigurationFiles_OverrideInCorrectOrder()
    {
        var injector = new InjectorBuilder()
            .AddModule(new TestModule())
            .AddModule(new OverridingConfigurationModule())
            .Build();
        
        var config = injector.Configuration.GetSection("test").Get<TestConfiguration>();
        config.Should().NotBeNull();
        config.ConfigurationProperty.Should().NotBeNull();
        config.ConfigurationProperty.Should().Be(42);
    }

    [Fact]
    public void ManualConfigurations_AreLoadedBeforeConfigure()
    {
        var module = new TestModule();
        module.Config.Should().BeNull();
        new InjectorBuilder()
            .AddModule(module)
            .AddModule(new ManualConfigurationModule())
            .Build();

        module.Config!.ConfigurationProperty.Should().Be(78);
    }

    private class OverridingConfigurationModule : InjectionModule
    {
        public override string[] ConfigurationFiles { get; } = new[] {"OverridingConfiguration.yaml"};

        public override void Configure(InjectorBuilder builder)
        {
        }
    }
    
    private class ManualConfigurationModule : InjectionModule
    {
        public override void PreConfigure(InjectorBuilder builder)
        {
            var section = builder.Configuration.GetSection("test");
            section["ConfigurationProperty"] = "78";
        }

        public override void Configure(InjectorBuilder builder)
        {
        }
    }
}