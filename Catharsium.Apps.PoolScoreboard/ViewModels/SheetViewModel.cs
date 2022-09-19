using Catharsium.Apps.PoolScoreboard.Core.Controllers;
using Catharsium.Apps.PoolScoreboard.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Catharsium.Apps.PoolScoreboard.ViewModels;

public partial class SheetViewModel : ObservableObject
{
    private readonly IGameStateController gameStateController;

    [ObservableProperty]
    IEnumerable<Turn> player1Turns;

    [ObservableProperty]
    IEnumerable<Turn> player2Turns;


    [ObservableProperty]
    IEnumerable<int> turns;


    public SheetViewModel(IGameStateController gameStateController) {
        this.gameStateController = gameStateController;
        this.Reload();
        MessagingCenter.Subscribe<MatchViewModel>(this, "Update", (sender) => {
            this.Reload();
        });
    }


    private void Reload() {
        var player1Id = this.gameStateController.GameState.Players[0].Id;
        this.Player1Turns = this.gameStateController.GameState.Turns.Where(t => t.PlayerId == player1Id);
        this.Player2Turns = this.gameStateController.GameState.Turns.Where(t => t.PlayerId != player1Id);
        this.Turns = this.gameStateController.GameState.Turns.Select(t => t.TurnNumber).Distinct().OrderBy(t => t).Select(t => t + 1);
    }
}