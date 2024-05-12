using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Common.Service;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using UraniumUI.Material.Attachments;

namespace MobileUI.Objects.ViewModels;

public partial class ScheduleEditorPageViewModel: ViewModelBase
{
    [ObservableProperty] private IBarberShopService barberShopService;

    public ScheduleEditorPageViewModel(IApplicationService appServiceProvider, ILogger logger)
        : base(appServiceProvider, logger)
    {
        barberShopService = appServiceProvider.BarberShopService;
    }
}