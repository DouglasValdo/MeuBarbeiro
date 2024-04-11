using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Common.Service;
using Microsoft.Extensions.Logging;

namespace MobileUI.Objects.ViewModels;

public partial class BarberTabViewModel : ViewModelBase
{
    [ObservableProperty] private bool _hasBarber = true;
    [ObservableProperty] private string? _barberCode;
    
    public BarberTabViewModel(IApplicationService appServiceProvider, ILogger logger) : base(appServiceProvider, logger)
    {
        
    }
}