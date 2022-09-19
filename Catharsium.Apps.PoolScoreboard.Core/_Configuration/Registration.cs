using Catharsium.Apps.PoolScoreboard.Core.Controllers;
using Catharsium.Apps.PoolScoreboard.Core.Models;
using Catharsium.Util.Configuration.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catharsium.Apps.PoolScoreboard.Core._Configuration;

public static class Registration
{
    public static IServiceCollection AddPoolScoreboardCore(this IServiceCollection services, IConfiguration configuration) {
        var settings = configuration.Load<PoolScoreboardCoreSettings>();

        return services.AddSingleton<PoolScoreboardCoreSettings, PoolScoreboardCoreSettings>(_ => settings)
            .AddSingleton(sp => new GameState("Player 1", "Player 2"))
            .AddSingleton<IGameStateController, GameStateController>();
    }
}