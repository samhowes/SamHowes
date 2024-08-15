using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SamHowes.Extensions.DependencyInjection.Modules;

public class Injector(IServiceProvider provider, IConfiguration configuration)
{
    public IServiceProvider Provider { get; } = provider;
    public IConfiguration Configuration { get; } = configuration;
    
    /// <summary>
    /// A proxy for <see cref="IServiceProvider.GetRequiredService"/>
    /// </summary>
    public T Get<T>() where T : notnull
    {
        return Provider.GetRequiredService<T>();
    }
}