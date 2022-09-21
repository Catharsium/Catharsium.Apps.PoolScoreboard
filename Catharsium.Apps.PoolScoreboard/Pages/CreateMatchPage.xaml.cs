using Catharsium.Apps.PoolScoreboard.ViewModels.Pages;

namespace Catharsium.Apps.PoolScoreboard.Pages;

public partial class CreateMatchPage : ContentPage
{
    public CreateMatchPage(CreateMatchViewModel viewModel) {
        InitializeComponent();
        BindingContext = viewModel;
    }
}