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
