using Catharsium.Apps.PoolScoreboard.ViewModels;

namespace Catharsium.Apps.PoolScoreboard.Views;

public partial class StatsPage : ContentPage
{
	public StatsPage(StatsViewModel viewModel) {
		this.InitializeComponent();
		this.BindingContext = viewModel;
	}
}