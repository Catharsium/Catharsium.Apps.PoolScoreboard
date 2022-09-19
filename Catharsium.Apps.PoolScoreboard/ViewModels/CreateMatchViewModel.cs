using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Catharsium.Apps.PoolScoreboard.ViewModels;

public partial class CreateMatchViewModel : ObservableObject
{
    [ObservableProperty]
    bool canStart;

    [ObservableProperty]
    string gameTypeIndex;

    [ObservableProperty]
    int raceTo;

    [ObservableProperty]
    string newPlayer;

    [ObservableProperty]
    ObservableCollection<string> players = new();


    [RelayCommand]
    void AddPlayer() {
        if (string.IsNullOrEmpty(this.NewPlayer)) {
            return;
        }

        this.Players.Add(this.NewPlayer);
        this.NewPlayer = "";

        this.CanStart = this.players.Count == 2 && gameTypeIndex != null;
    }


    [RelayCommand]
    void Delete(string player) {
        this.Players.Remove(player);
        this.CanStart = this.players.Count == 2 && gameTypeIndex != null;
    }
}