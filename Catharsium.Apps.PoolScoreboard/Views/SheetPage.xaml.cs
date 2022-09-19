using Catharsium.Apps.PoolScoreboard.ViewModels;

namespace Catharsium.Apps.PoolScoreboard.Views;

public partial class SheetPage : ContentPage
{
    public SheetPage(SheetViewModel viewModel) {
        this.InitializeComponent();
        this.BindingContext = viewModel;
    }
}