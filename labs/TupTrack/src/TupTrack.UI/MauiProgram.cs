using Microsoft.Extensions.Logging;
using TupTrack.SensorServices;
using TupTrack.UseCases.Interfaces;
using TupTrack.UseCases.SensorCoordinator;
using TupTrack.UseCases;

using UC = TupTrack.UseCases;

namespace TupTrack.UI;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<ISensorService, BarometerService>();
        builder.Services.AddSingleton<ISensorService, AccelerometrService>();

		builder.Services.AddSingleton<ISensorCoordinator, SensorCoordinator>();



        builder.Services.AddSingleton<IStartRecordingUC, StartRecordingUC>();


        builder.Services.AddSingleton<UC.IApplication, UC.Application>();


        return builder.Build();
	}
}
