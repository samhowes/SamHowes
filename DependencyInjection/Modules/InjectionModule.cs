namespace SamHowes.Extensions.DependencyInjection.Modules;

public abstract class InjectionModule
{
    public virtual List<Type> Dependencies { get; } = new List<Type>();
    public virtual IEnumerable<string> ConfigurationFiles { get; } = Array.Empty<string>();
    public virtual void PreConfigure(InjectorBuilder builder){}
    public abstract void Configure(InjectorBuilder builder);

    public void Deps<T>() where T : InjectionModule
    {
        Deps(typeof(T));
    }

    public void Deps(params Type[] types)
    {
        Dependencies.AddRange(types);
    }
}