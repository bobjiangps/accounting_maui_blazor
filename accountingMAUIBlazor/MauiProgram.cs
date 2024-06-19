using Microsoft.Extensions.Logging;
using accountingMAUIBlazor.Data;
using accountingMAUIBlazor.Services;

namespace accountingMAUIBlazor;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder.UseMauiApp<App>().ConfigureFonts(fonts =>{
			fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
		});

        //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://127.0.0.1:8000/external/api/") });
        builder.Services.AddSingleton<IUserService, UserService>();

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<WeatherForecastService>();

		return builder.Build();
	}
}

