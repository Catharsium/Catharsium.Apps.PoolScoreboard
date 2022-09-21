using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Catharsium.Apps.PoolScoreboard.ViewModels.Components;

[INotifyPropertyChanged]
public partial class NumericPopupViewModel
{
    public int Value => int.TryParse(this.Number, out var result) ? result : 0;


    [ObservableProperty]
    NumericPopupSettings settings;

    [ObservableProperty]
    string number;


    public NumericPopupViewModel() {
        this.Settings = new NumericPopupSettings { Title = "Enter a number" };
    }


    [RelayCommand]
    void Add(string number) {
        Number += number;
    }


    [RelayCommand]
    void Delete() {
        if (this.Number.Length > 0) {
            this.Number = this.Number[..^1];
        }
    }
}