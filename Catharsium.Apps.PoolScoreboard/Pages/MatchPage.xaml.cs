using Catharsium.Apps.PoolScoreboard.ViewModels.Pages;

namespace Catharsium.Apps.PoolScoreboard.Pages;

public partial class MatchPage : ContentPage
{
    public MatchPage(MatchViewModel viewModel) {
        this.InitializeComponent();
        this.BindingContext = viewModel;
    }
}