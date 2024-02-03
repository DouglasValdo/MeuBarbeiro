using CommunityToolkit.Mvvm.ComponentModel;

namespace MobileUI.Objects.ViewModels;

public partial class AppPopUpViewModel : ObservableObject
{
    [ObservableProperty] private string _messageText = string.Empty;
    [ObservableProperty] private bool _isBusy = false;

}