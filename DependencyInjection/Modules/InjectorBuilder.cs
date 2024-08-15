using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SamHowes.Extensions.DependencyInjection.Modules;

public class InjectorBuilder(IServiceCollection services, ConfigurationManager configuration)
{
    private readonly List<InjectionModule> _modules = [];
    public InjectorBuilder() : this(new ServiceCollection(), new ConfigurationManager()) {}
    
    public IServiceCollection Services { get; } = services;
    public IConfigurationManager Configuration { get; } = configuration;

    public InjectorBuilder AddModule(InjectionModule module)
    {
        _modules.Add(module);
        return this;
    }
    
    /// <summary>
    /// A proxy for <see cref="IServiceCollection.AddScoped"/>
    /// </summary>
    public InjectorBuilder Add<T>() where T : class
    {
        Services.AddScoped<T>();
        return this;
    }

    public Injector Build()
    {
        foreach (var module in _modules)
        {
            module.Configure(this);
        }
        
        var provider = Services.BuildServiceProvider();
        var configuration = Configuration.Build();
        
        return new Injector(provider, configuration);
    }
}