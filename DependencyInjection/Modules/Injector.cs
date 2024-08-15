using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SamHowes.Extensions.DependencyInjection.Modules;

public class Injector(IServiceProvider provider, IConfigurationRoot configuration)
{
    public class InjectorScope(Injector injector, IServiceScope innerScope) : IDisposable
    {
        private static int _scopeNumber = 0;
        
        public int ScopeId { get; } = ++_scopeNumber;
        public IServiceProvider Provider { get; }= innerScope.ServiceProvider;
        public void Dispose()
        {
            innerScope.Dispose();
            injector.PopScope(this);
        }
    }

    public IServiceProvider RootProvider { get; } = provider;
    private readonly Stack<InjectorScope> _scopeStack = [];

    public IServiceProvider Provider { get; private set; } = provider;
    public IConfigurationRoot Configuration { get; } = configuration;
    
    /// <summary>
    /// A proxy for <see cref="IServiceProvider.GetRequiredService"/>
    /// </summary>
    public T Get<T>() where T : notnull
    {
        return Provider.GetRequiredService<T>();
    }

    public InjectorScope StartScope()
    {
        var scope = Provider.CreateScope();
        var injectorScope = new InjectorScope(this, scope);
        Provider = injectorScope.Provider;
        _scopeStack.Push(injectorScope);
        return injectorScope;
    }
    
    private void PopScope(InjectorScope injectorScope)
    {
        var popped = this._scopeStack.Pop();
        if (popped != injectorScope)
            throw new Exception("Scopes were popped out of order");

        Provider = _scopeStack.TryPeek(out var newProvider) 
            ? newProvider.Provider : RootProvider;
    }
}