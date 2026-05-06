using Microsoft.Extensions.Logging;
using TupTrack.Infrastructure;
using TupTrack.SensorServices;
using TupTrack.UseCases;
using TupTrack.UseCases.SensorCoordinator;
using UC = TupTrack.UseCases;
using TupTrack.UseCases.Handlers;
using MP = TupTrack.UI.MainPage;
using TupTrack.UseCases.Repositories;
using TupTrack.Infrastructure.Repositories;

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
        SQLitePCL.Batteries_V2.Init();

        var databasePath = Path.Combine(
            FileSystem.AppDataDirectory,
            "tuptrack.db3");

        builder.Services.AddSingleton(new DatabaseContext(databasePath));


		// Sensors
        builder.Services.AddSingleton<ISensorService, BarometerService>();
        builder.Services.AddSingleton<ISensorService, AccelerometerService>();

		builder.Services.AddSingleton<ISensorCoordinator, SensorCoordinator>();

		// handlers
        builder.Services.AddSingleton<StartRecordingHandler>();
		builder.Services.AddSingleton<GetRecordingOptionsHandler>();

        // infrustructure
        builder.Services.AddSingleton<IRecordingRepository, RecordingRepository>();
		builder.Services.AddSingleton<IMainPageRepository, MainPageRepository>();

        // UI
        builder.Services.AddSingleton<TupTrack.UI.StatisticsPage.StatisticsPageViewModel>();
        builder.Services.AddTransient<TupTrack.UI.StatisticsPage.StatisticsPage>();

        builder.Services.AddSingleton<MP.MainPageViewModel>();
        builder.Services.AddTransient<MP.MainPage>();


        return builder.Build();
	}
}
