using Catharsium.Apps.PoolScoreboard.Core.Controllers;
using Catharsium.Apps.PoolScoreboard.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Catharsium.Apps.PoolScoreboard.ViewModels;

public partial class CreateMatchViewModel : ObservableObject
{
    private readonly IGameStateController gameStateController;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(StartMatchCommand))]
    string matchTypeIndex;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(StartMatchCommand))]
    int raceTo;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddPlayerCommand))]
    string newPlayer;

    [ObservableProperty]
    ObservableCollection<string> players = new();


    public CreateMatchViewModel(IGameStateController gameStateController) {
        this.gameStateController = gameStateController;
    }


    [RelayCommand(CanExecute = "CanAddPlayer")]
    void AddPlayer() {
        this.Players.Add(this.NewPlayer);
        this.NewPlayer = "";
        this.StartMatchCommand.NotifyCanExecuteChanged();
    }

    bool CanAddPlayer() {
        return !string.IsNullOrEmpty(this.NewPlayer);
    }


    [RelayCommand]
    void DeletePlayer(string player) {
        this.Players.Remove(player);
        this.StartMatchCommand.NotifyCanExecuteChanged();
    }


    [RelayCommand(CanExecute = "CanStartMatch")]
    async void StartMatch() {
        this.gameStateController.Match = new StraightPoolMatch(this.RaceTo, this.Players.Take(2).Select(p => new Player(p)));
        await Shell.Current.GoToAsync("//current/match");
    }

    bool CanStartMatch() {
        return MatchTypeIndex != null
            && this.RaceTo > 0
            && this.Players.Count == 2;
    }
}