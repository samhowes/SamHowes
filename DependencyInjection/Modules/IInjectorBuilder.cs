using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SamHowes.Extensions.DependencyInjection.Modules;

public interface IInjectorBuilder<out T>
{
    IServiceCollection Services { get; }

    IConfigurationManager Configuration { get; }

    InjectorBuilder AddModule(InjectionModule module);
    InjectorBuilder AddModules(params InjectionModule[] modules);

    InjectorBuilder Add<TModule>() where TModule : class;

    T Build();
}