using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Domain.Common.Service;
using Microsoft.Extensions.Logging;
using MobileUI.Objects.Session;
// ReSharper disable All

namespace MobileUI.Objects.ViewModels;

public partial class SettingsTabViewModel : ViewModelBase
{
    public ICommand ClearSessionCommand { get; } = new RelayCommand(ClearSession);
    
    public SettingsTabViewModel(IApplicationService appServiceProvider, ILogger logger) : base(appServiceProvider, logger)
    { 
    }

    private static void ClearSession()
    {
        SessionManager.ClearUser();
    }
}