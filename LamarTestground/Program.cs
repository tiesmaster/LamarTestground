using Lamar.Microsoft.DependencyInjection;

using LamarTestground;

var builder = CreateHostBuilder(args);

builder.UseLamar(registry =>
{
    registry.IncludeRegistry<LamarRegistry>();
});

builder
    .Build()
    .Run();

IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, config) =>
        {
        })
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });