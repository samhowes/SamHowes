namespace SamHowes.Extensions.DependencyInjection.Modules.Web;

public class WebInjectorBuilder(WebApplicationBuilder builder) 
    : IInjectorBuilder<WebApplication>
{
    private readonly InjectorBuilder _injectorBuilder = new(builder.Services, builder.Configuration);
    public IServiceCollection Services => builder.Services;
    public IConfigurationManager Configuration => builder.Configuration;
    
    public InjectorBuilder AddModule(InjectionModule module)
    {
        return _injectorBuilder.AddModule(module);
    }

    public InjectorBuilder Add<T>() where T : class
    {
        return _injectorBuilder.Add<T>();
    }

    public WebApplication Build()
    {
        _injectorBuilder.ApplyModules();
        return builder.Build();
    }
}
