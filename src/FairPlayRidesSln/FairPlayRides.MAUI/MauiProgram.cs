using Microsoft.AspNetCore.Components.WebView.Maui;
using FairPlayRides.MAUI.Data;
using FairPlayRides.Blazor.Shared.GeoLocation;
using FairPlayRides.MAUI.AgnosticImplementations;

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

		builder.Services.AddBlazorWebView();
		builder.Services.AddSingleton<IGeoLocationProvider, GeoLocationProvider>();
		builder.Services.AddSingleton<WeatherForecastService>();

		return builder.Build();
	}
}
