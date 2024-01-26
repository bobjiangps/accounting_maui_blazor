using Microsoft.Extensions.Logging;
using accountingMAUIBlazor.Data;
using accountingMAUIBlazor.Services;

namespace accountingMAUIBlazor;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();
		builder.Services.AddBootstrapBlazor();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<WeatherForecastService>();
		//builder.Services.AddSingleton<PoetryStorage>();
		builder.Services.AddScoped<IPoetryStorage, PoetryStorage>();

		return builder.Build();
	}
}

