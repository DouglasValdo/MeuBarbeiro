using CommunityToolkit.Mvvm.ComponentModel;

namespace MobileUI.Objects.ViewModels;

public partial class AppModalViewModel : ObservableObject
{
    [ObservableProperty] private string _messageText = string.Empty;
    [ObservableProperty] private bool _isBusy = false;

}