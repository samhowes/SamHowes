using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SamHowes.Extensions.Configuration.Yaml;

namespace SamHowes.Extensions.DependencyInjection.Modules;

public class InjectorBuilder(IServiceCollection services, ConfigurationManager configuration)
    : IInjectorBuilder<Injector>
{
    private readonly List<InjectionModule> _modules = [];
    public InjectorBuilder() : this(new ServiceCollection(), new ConfigurationManager()) {}
    
    public IServiceCollection Services { get; } = services;
    public IConfigurationManager Configuration { get; } = configuration;

    public InjectorBuilder AddModules(params InjectionModule[] modules)
    {
        foreach (var module in modules)
        {
            AddModule(module);
        }

        return this;
    }
    
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
        ApplyModules();
        
        Services.AddSingleton<Injector>();
        
        var provider = Services.BuildServiceProvider();
        var configuration = Configuration.Build();
        
        return new Injector(provider, configuration);
    }

    public void ApplyModules()
    {
        var basePath = Path.GetDirectoryName(typeof(InjectorBuilder).Assembly.Location)!;
        foreach (var module in _modules)
        {
            foreach (var filename in module.ConfigurationFiles)
            {
                var path = Path.Combine(basePath, filename);
                switch (Path.GetExtension(path))
                {
                    case ".json":
                        Configuration.AddJsonFile(path);
                        break;
                    case ".yaml":
                    case ".yml":
                        Configuration.AddYamlFile(path);
                        break;
                    default:
                        throw new Exception($"Unsupported configuration file extension: {Path.GetExtension(path)}");
                }
            }
            module.PreConfigure(this);
        }
        
        foreach (var module in _modules)
        {
            module.Configure(this);
        }
    }
}