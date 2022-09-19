using Catharsium.Apps.PoolScoreboard.Core.Controllers;
using Catharsium.Apps.PoolScoreboard.Core.Models;
using Catharsium.Apps.PoolScoreboard.ViewModels;

namespace Catharsium.Apps.PoolScoreboard.Pages;

public partial class CreateMatchPage : ContentPage
{
    private readonly CreateMatchViewModel viewModel;
    private readonly IGameStateController gameStateController;


    public CreateMatchPage(CreateMatchViewModel viewModel, IGameStateController gameStateController) {
        InitializeComponent();
        BindingContext = viewModel;
        this.viewModel = viewModel;
        this.gameStateController = gameStateController;
    }


    private async void StartMatchEvent(object sender, EventArgs e) {
        this.gameStateController.GameState = new StraightPoolMatch(viewModel.RaceTo, viewModel.Players.Take(2).Select(p => new Player(p)));
        await Shell.Current.GoToAsync("//current/match");
    }
}