using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MobileUI.Objects.ViewModels;

public partial class ConfirmModalViewModel : ObservableObject
{
    [ObservableProperty] private string? _title;
    [ObservableProperty] private string? _message;
    [ObservableProperty] private string _confirm = "OK";
    [ObservableProperty] private string _cancel = "Cancel";
    
    public ICommand? ConfirmCommand { get; set; }
    public ICommand? CancelCommand { get; set; }

}