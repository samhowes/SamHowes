namespace SamHowes.Extensions.DependencyInjection.Modules;

public abstract class InjectionModule
{
    public virtual IEnumerable<string> ConfigurationFiles { get; } = Array.Empty<string>();
    public virtual void PreConfigure(InjectorBuilder builder){}
    public abstract void Configure(InjectorBuilder builder);
}