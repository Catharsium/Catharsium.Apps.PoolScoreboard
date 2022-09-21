using Catharsium.Apps.PoolScoreboard.Core.Controllers;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Catharsium.Apps.PoolScoreboard.ViewModels.Pages;

public class StatsViewModel : ObservableObject
{
    public readonly IGameStateController GameStateController;


    public StatsViewModel(IGameStateController gameStateController) {
        GameStateController = gameStateController;
    }
}