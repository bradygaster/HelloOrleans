using HelloOrleans.Grains;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseOrleans(siloBuilder =>
{
    siloBuilder
        .Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "Cluster";
            options.ServiceId = "Service";
        })
        .Configure<SiloOptions>(options =>
        {
            options.SiloName = "Dashboard";
        })
        .ConfigureEndpoints(siloPort: 11_112, gatewayPort: 30_001)
        .UseAzureStorageClustering(options => options.ConfigureTableServiceClient(builder.Configuration.GetValue<string>("StorageConnectionString")))
        .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(HelloGrain).Assembly).WithReferences())
        .UseDashboard()
        ;
});

var app = builder.Build();

app.MapGet("/", () => Results.Ok("Dashboard"));

app.Run();
