using CommunityToolkit.Maui;
using LocalizationResourceManager.Maui;
using Microsoft.Extensions.Logging;
using Serilog;
using signals.Services;
using signals.ViewModels;

#if IOS || MACCATALYST
using Microsoft.Maui.Controls.Handlers.Items2;
#endif

namespace signals;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		
		builder.Services.AddSerilog(
        	new LoggerConfiguration()
            	.WriteTo.File(Path.Combine(FileSystem.Current.AppDataDirectory, "logs", "log.txt"),
				 	rollingInterval: RollingInterval.Day,
                	fileSizeLimitBytes: 10000000)
            	.CreateLogger()
    	);

		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit(options =>
			{
				options.SetShouldEnableSnackbarOnWindows(true);
			}).ConfigureFonts(fonts =>
			{
				fonts.AddFont("FluentSystemIcons-Regular.ttf", "FluentUI"); //https://fluenticons.co
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<IDeviceDisplay>(DeviceDisplay.Current);
		builder.Services.AddSingleton<IDataService, DataService>();

		builder.Services.AddSingleton<IDeviceInfo>(DeviceInfo.Current);
		
		builder.Services.AddSingleton<MainViewModel>();
		
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<AppShell>();
#if DEBUG
		builder.Logging.AddDebug();
#endif

#if IOS || MACCATALYST

		builder.ConfigureMauiHandlers(handlers =>
			{
				handlers.AddHandler<CollectionView, CollectionViewHandler2>();
				handlers.AddHandler<CarouselView, CarouselViewHandler2>();
			});
#endif

		builder.UseLocalizationResourceManager(settings =>
		{
			settings.AddResource(Resources.Signals.ResourceManager);
			settings.RestoreLatestCulture(true);
			settings.SuppressTextNotFoundException(true);
		});


		return builder.Build();
	}
}
