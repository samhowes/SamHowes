# SamHowes.Extensions.DependencyInjection.Modules

Simple modules to encapsulate your `IServiceCollection` and `IConfiguration` configurations

```csharp
// Declare your configuration in focused modules
public class LocalConfigurationModule : InjectionModule
{
    public override void PreConfigure(InjectorBuilder builder)
    {
        builder.Configuration["ConnectionString"] = "Server=(localdb)\\MsSqlLocalDb";        
    }
    
    public override void Configure(InjectorBuilder builder) {}    
}

public class MyModule : InjectionModule
{
    public override IEnumerable<string> ConfigurationFiles { get; } = new [] { "ServiceConfig.yaml" };
    
    public override void Configure(InjectorBuilder builder)
    {
        builder.Services.AddScoped<Service>();
        builder.Services.Add(builder.Configuration.GetSection("Service").Get<ServiceConfig>();
    }
}

public class DatabaseModule : InjectionModule 
{
    public override void Configure(InjectorBuilder builder)
    {
        var connectionString = builder.Configuration.GetSection("ConnectionString").Value;
        // connectionString will be the Prod connection string because PreConfigure is called in module order before 
        // Configure is called
        builder.Services.AddDbContext<MyDbContext>(options => 
            options.UseSqlServer(connectionString));
    }
}

public class ProdConfigurationModule : InjectionModule
{
    public override void PreConfigure(InjectorBuilder builder)
    {
        builder.Configuration["ConnectionString"] = "Server=ProdServer.mydomain.com";
    }

    public override void Configure(InjectorBuilder builder)
    {
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