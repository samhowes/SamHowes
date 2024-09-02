# SamHowes.Extensions.DependencyInjection.Modules.Web
Provides a simple wrapper around WebApplicationBuilder to provide a clean drop-in for InjectionModules

Usage
```csharp
var builder = new WebInjectorBuilder(WebApplication.CreateBuilder(args));

builder.AddModule(new MyModule());

// calls through to the WebApplicationBuilder and returns the built WebApplication
var app = builder.Build();
```