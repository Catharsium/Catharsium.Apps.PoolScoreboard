namespace Catharsium.Apps.PoolScoreboard.Pages;

public partial class NewMatchPage : ContentPage
{
	public NewMatchPage() {
		InitializeComponent();

        btnStart.Clicked += async (s, e) => await Shell.Current.GoToAsync("//current/match", false);
    }
}