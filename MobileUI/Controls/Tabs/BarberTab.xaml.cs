using Domain.Common.Service;
using Microsoft.Extensions.Logging;
using MobileUI.Objects.ViewModels;

namespace MobileUI.Controls.Tabs;

public partial class BarberTab : ContentPage
{
    public BarberTab(IApplicationService applicationService, ILogger<BarberTab> logger)
    {
        InitializeComponent();
        BindingContext = new BarberTabViewModel(applicationService, logger);
    }
}