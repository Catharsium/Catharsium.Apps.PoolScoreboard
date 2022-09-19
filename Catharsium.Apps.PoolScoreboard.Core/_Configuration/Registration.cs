using Catharsium.Apps.PoolScoreboard.Core.Controllers;
using Catharsium.Util.Configuration.Extensions;
using Microsoft.Extensions.Configuration;

namespace Catharsium.Apps.PoolScoreboard.Core._Configuration;

public static class Registration
{
    public static IServiceCollection AddPoolScoreboardCore(this IServiceCollection services, IConfiguration configuration) {
        var settings = configuration.Load<PoolScoreboardCoreSettings>();

        return services.AddSingleton<PoolScoreboardCoreSettings, PoolScoreboardCoreSettings>(_ => settings)
            .AddSingleton<IGameStateController, GameStateController>();
    }
}