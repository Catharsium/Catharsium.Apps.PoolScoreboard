using Catharsium.Apps.PoolScoreboard.Core._Configuration;
using Catharsium.Apps.PoolScoreboard.Pages;
using Catharsium.Apps.PoolScoreboard.ViewModels;
using Catharsium.Util.Configuration.Extensions;
using Microsoft.Extensions.Configuration;

namespace Catharsium.Apps.PoolScoreboard._Configuration;

public static class Registration
{
    public static IServiceCollection AddPoolScoreboardApp(this IServiceCollection services, IConfiguration configuration) {
        var settings = configuration.Load<PoolScoreboardCoreSettings>();

        return services.AddSingleton<PoolScoreboardCoreSettings, PoolScoreboardCoreSettings>(_ => settings)
            .AddSingleton<MatchPage>()
            .AddSingleton<MatchViewModel>()
            .AddSingleton<SheetPage>()
            .AddSingleton<SheetViewModel>()
            .AddSingleton<StatsPage>()
            .AddSingleton<StatsViewModel>();
    }
}