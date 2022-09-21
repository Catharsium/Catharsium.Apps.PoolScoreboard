using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Catharsium.Apps.PoolScoreboard.ViewModels.Components;

[INotifyPropertyChanged]
public sealed partial class NumericPopupViewModel
{
    [ObservableProperty]
    string title;

    [ObservableProperty]
    string value;


    public NumericPopupViewModel(string title) {
        this.Title = title;
    }


    [RelayCommand]
    void Add(string number) {
        Value += number;
    }


    [RelayCommand]
    void Delete() {
        if (this.Value.Length > 0) {
            this.Value = this.Value[..^1];
        }
    }
}