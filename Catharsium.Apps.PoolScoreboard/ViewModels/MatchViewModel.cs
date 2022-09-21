using Catharsium.Apps.PoolScoreboard.Components;
using Catharsium.Apps.PoolScoreboard.Core.Controllers;
using Catharsium.Apps.PoolScoreboard.Core.Events.Complex;
using Catharsium.Apps.PoolScoreboard.Core.GameActions;
using Catharsium.Apps.PoolScoreboard.Core.Models;
using Catharsium.Apps.PoolScoreboard.Core.Models.Views;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Catharsium.Apps.PoolScoreboard.ViewModels;

public partial class MatchViewModel : ObservableObject
{
    private readonly IGameStateController gameManager;

    [ObservableProperty]
    string player1Name;
    [ObservableProperty]
    int player1Score;
    [ObservableProperty]
    string player2Name;
    [ObservableProperty]
    int player2Score;
    [ObservableProperty]
    ScoreView scoreView;


    public MatchViewModel(IGameStateController gameManager) {
        this.gameManager = gameManager;
        this.Reload();
    }


    [RelayCommand]
    async Task EndTurn(ShotType shotType) {
        var ballsOnTable = await this.DisplayPopup("Remaining balls");
        var foulPoints = 0;
        if (shotType == ShotType.Foul) {
            foulPoints = 1;
            if (this.gameManager.IsCurrentPlayerOnTwoFouls()) {
                foulPoints = 15;
            }
        }

        if (shotType == ShotType.FoulBreak) {
            foulPoints = 2;
        }

        this.gameManager.AddNewEvent(new EndTurnEvent(shotType, ballsOnTable, foulPoints));

        this.Reload();
    }


    [RelayCommand]
    void Score(int? ballsPotted) {
        if (!ballsPotted.HasValue) {
            this.gameManager.AddNewEvent(new ClearRackEvent());
        }
        else {
            this.gameManager.AddNewEvent(new ScoreEvent(ballsPotted.Value));
        }

        this.Reload();
    }


    [RelayCommand]
    public void Undo() {
        this.gameManager.UndoLastEvent();
        this.Reload();
    }


    public async Task<int> DisplayPopup(string message) {
        var popup = new NumericPopup(new Components.NumericPopupViewModel(message)) {
            CanBeDismissedByTappingOutsideOfPopup = true
        };

        var result = await Shell.Current.CurrentPage.ShowPopupAsync(popup);

        return result is int ballsOnTable ? ballsOnTable : 0;
    }


    void Reload() {
        var scoreView = this.gameManager.GetScoreView();
        MessagingCenter.Send(this, "Update");
        ScoreView = scoreView;
        Player1Name = scoreView.PlayerScores[0].Name;
        Player1Score = scoreView.PlayerScores[0].Score;
        Player2Name = scoreView.PlayerScores[1].Name;
        Player2Score = scoreView.PlayerScores[1].Score;
    }
}