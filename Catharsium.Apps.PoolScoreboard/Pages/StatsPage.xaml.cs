using Catharsium.Apps.PoolScoreboard.ViewModels;
using System.Text.Json;

namespace Catharsium.Apps.PoolScoreboard.Pages;

public partial class StatsPage : ContentPage
{
    private readonly StatsViewModel viewModel;

    public StatsPage(StatsViewModel viewModel) {
        this.InitializeComponent();
        this.BindingContext = viewModel;
        this.viewModel = viewModel;
    }

    private async void Button_Clicked(object sender, EventArgs e) {
        var filePath = Path.Combine(FileSystem.CacheDirectory, "match.json");
        var stream = File.OpenWrite(filePath);
        JsonSerializer.Serialize(stream, this.viewModel.GameStateController.GameState, this.viewModel.GameStateController.GameState.GetType());

        await Share.Default.RequestAsync(new ShareFileRequest {
            Title = "Share text file",
            File = new ShareFile(filePath)
        });
    }
}