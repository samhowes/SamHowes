namespace SamHowes.Extensions.DependencyInjection.Modules;

public abstract class InjectionModule
{
    public abstract void Configure(InjectorBuilder injectorBuilder);
}