using Catharsium.Apps.PoolScoreboard._Configuration;
using Catharsium.Apps.PoolScoreboard.Core._Configuration;

namespace Catharsium.Apps.PoolScoreboard;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp() {
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts => {
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddPoolScoreboardApp(builder.Configuration);
		builder.Services.AddPoolScoreboardCore(builder.Configuration);

		return builder.Build();
	}
}