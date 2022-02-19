using FairPlayRides.Blazor.Client;
using FairPlayRides.Blazor.Client.CustomImplementations;
using FairPlayRides.Blazor.Shared.GeoLocation;
using FairPlayRides.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var azureMapsConfiguration = builder.Configuration.GetSection("AzureMapsConfiguration").
    Get<AzureMapsConfiguration>();
builder.Services.AddSingleton(azureMapsConfiguration);
builder.Services.AddSingleton<IGeoLocationProvider, WebGeoLocationProvider>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
