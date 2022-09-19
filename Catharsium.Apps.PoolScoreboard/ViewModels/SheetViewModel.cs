using Catharsium.Apps.PoolScoreboard.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Catharsium.Apps.PoolScoreboard.ViewModels;

public partial class SheetViewModel : ObservableObject
{
    public SheetViewModel(GameState gameState) {
        this.gameState = gameState;
        this.Reload();
        MessagingCenter.Subscribe<MatchViewModel>(this, "Update", (sender) => {
            this.Reload();
        });
    }

    private void Reload() {
        var player1Id = this.gameState.Players[0].Id;
        this.Player1Turns = this.gameState.Turns.Where(t => t.PlayerId == player1Id);
        this.Player2Turns = this.gameState.Turns.Where(t => t.PlayerId != player1Id);
        this.Turns = this.gameState.Turns.Select(t => t.TurnNumber).Distinct().OrderBy(t => t).Select(t => t + 1);
    }

    [ObservableProperty]
    IEnumerable<Turn> player1Turns;

    [ObservableProperty]
    IEnumerable<Turn> player2Turns;


    [ObservableProperty]
    IEnumerable<int> turns;
    private readonly GameState gameState;
}