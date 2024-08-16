# SamHowes.Extensions

Extensions in the convention of Microsoft.Extensions

## Packages

### SamHowes.Extensions.Configuration.Yaml

An IConfigurationBuilder extension using YamlDotnet in the format
of [Microsoft.Extensions.Configuration.Json](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.Json)

```c#
using SamHowes.Extensions.Configuration.Yaml;

IConfiguration configuration = new ConfigurationBuilder()
    .AddYamlFile("config.yaml")
    .Build();
```

### SamHowes.Extensions.DependencyInjection.Modules

Simple modules to encapsulate your `IServiceCollection` configurations
```csharp
var injector = new InjectorBuilder()
    .AddModule(new MyModule())
    .AddModule(new DatabaseModule())
    .Build();

var service = injector.Get<MyDbContext>();

```