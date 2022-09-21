using Catharsium.Apps.PoolScoreboard._Configuration;
using CommunityToolkit.Maui;

namespace Catharsium.Apps.PoolScoreboard;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp() {
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts => {
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddPoolScoreboardApp(builder.Configuration);

		return builder.Build();
	}
}