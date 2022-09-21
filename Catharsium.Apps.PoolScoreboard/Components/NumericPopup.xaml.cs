using Catharsium.Apps.PoolScoreboard.ViewModels.Components;

namespace Catharsium.Apps.PoolScoreboard.Components;

public partial class NumericPopup : CommunityToolkit.Maui.Views.Popup
{
    private readonly NumericPopupViewModel viewModel;


    public NumericPopup(NumericPopupViewModel viewModel) {
        InitializeComponent();
        this.BindingContext = viewModel;
        this.viewModel = viewModel;
    }


    void OnOkButtonClicked(object sender, EventArgs e) => Close(int.Parse(this.viewModel.Value));
}