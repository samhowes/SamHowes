# SamHowes.Extensions.DependencyInjection.Modules

Simple modules to encapsulate your `IServiceCollection` configurations

```csharp
public class Service {}

// Declare your configuration in focused modules
public class MyModule : InjectionModule
{
    public override void Configure(InjectorBuilder builder)
    {
        builder.Services.AddScoped<Service>();
    }
}

public class DatabaseModule : InjectionModule 
{
    public override void Configure(InjectorBuilder builder)
    {
        builder.Services.AddDbContext<MyDbContext>();
    }
}

// reuse them in any point in your code 
var injector = new InjectorBuilder()
    .AddModule(new MyModule())
    .AddModule(new DatabaseModule())
    .Build();

// retrieve your service using the built IServiceProvider on Injector
var myService = injector.Provider.GetRequiredService<Service>();

// shorter syntax convenience method
var myDbContext = injector.Get<MyDbContext>();
using (var scope = injector.StartScope())
{
    // injector's Provider property is auto-updated to the current scope's priority
    var scopedMyService = injector.Get<Service>();
    // scopedMySerivce is a different instance than myService!
}

// outside of the using block, injector.Provider is reset back to injector.RootProvider

```