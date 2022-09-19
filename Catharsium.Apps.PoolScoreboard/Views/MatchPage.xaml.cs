using Catharsium.Apps.PoolScoreboard.ViewModels;

namespace Catharsium.Apps.PoolScoreboard.Views;

public partial class MatchPage : ContentPage
{
    public MatchPage(MatchViewModel viewModel) {
        this.InitializeComponent();
        this.BindingContext = viewModel;
    }
}