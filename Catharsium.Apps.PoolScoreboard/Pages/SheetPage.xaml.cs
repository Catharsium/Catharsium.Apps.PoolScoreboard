using Catharsium.Apps.PoolScoreboard.ViewModels.Pages;

namespace Catharsium.Apps.PoolScoreboard.Pages;

public partial class SheetPage : ContentPage
{
    public SheetPage(SheetViewModel viewModel) {
        this.InitializeComponent();
        this.BindingContext = viewModel;
    }
}