using Microsoft.AspNetCore.Components.WebView.Maui;
using FairPlayRides.MAUI.Data;
using FairPlayRides.Blazor.Shared.GeoLocation;
using FairPlayRides.MAUI.AgnosticImplementations;
using Microsoft.Extensions.Configuration;
using FairPlayRides.Components;
using System.Reflection;
using Microsoft.AspNetCore.Components.Web;

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

		string appSettingsResourceName = string.Empty;
		var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MauiProgram)).Assembly;
#if DEBUG
		appSettingsResourceName = "FairPlayRides.MAUI.wwwroot.appsettings.Development.json";
#else
		appSettingsResourceName = "FairPlayRides.MAUI.wwwroot.appsettings.json";
#endif
		Stream stream = assembly.GetManifestResourceStream(appSettingsResourceName);
		builder.Configuration.AddJsonStream(stream);
        AzureMapsConfiguration azureMapsConfiguration = builder.Configuration
            .GetSection(nameof(AzureMapsConfiguration))
            .Get<AzureMapsConfiguration>();
        builder.Services.AddSingleton(azureMapsConfiguration);
        builder.Services.AddBlazorWebView();
		builder.Services.AddSingleton<IGeoLocationProvider, GeoLocationProvider>();
		builder.Services.AddSingleton<WeatherForecastService>();
		builder.Services.AddSingleton<IErrorBoundaryLogger, ErrorBoundaryLogger>();

		return builder.Build();
	}
}

public class ErrorBoundaryLogger : IErrorBoundaryLogger
{
    public ValueTask LogErrorAsync(Exception exception)
    {
		return ValueTask.CompletedTask;
    }
}
