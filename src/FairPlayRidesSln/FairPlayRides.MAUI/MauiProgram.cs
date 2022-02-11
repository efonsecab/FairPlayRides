using Microsoft.AspNetCore.Components.WebView.Maui;
using FairPlayRides.MAUI.Data;
using FairPlayRides.Blazor.Shared.GeoLocation;
using FairPlayRides.MAUI.AgnosticImplementations;
using Microsoft.Extensions.Configuration;
using FairPlayRides.Components;

namespace FairPlayRides.MAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.RegisterBlazorMauiWebView()
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});
		var appParentDirectory = Directory.GetParent(AppContext.BaseDirectory).Parent;
		var appSettingsFilePath = Path.Combine(appParentDirectory.FullName, "wwwroot/appsettings.json");
		builder.Configuration.AddJsonFile(appSettingsFilePath, optional:false);
		AzureMapsConfiguration azureMapsConfiguration = builder.Configuration
			.GetSection(nameof(AzureMapsConfiguration))
			.Get<AzureMapsConfiguration>();
		builder.Services.AddSingleton(azureMapsConfiguration);
		builder.Services.AddBlazorWebView();
		builder.Services.AddSingleton<IGeoLocationProvider, GeoLocationProvider>();
		builder.Services.AddSingleton<WeatherForecastService>();

		return builder.Build();
	}
}
