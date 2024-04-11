using Domain.Common.Service;
using Microsoft.Extensions.Logging;
using MobileUI.Objects.ViewModels;

namespace MobileUI.Controls.Tabs;

public partial class SettingsTab : ContentPage
{
    public SettingsTab(IApplicationService applicationService, ILogger<SettingsTab> logger)
    {
        InitializeComponent();
        BindingContext = new SettingsTabViewModel(applicationService, logger);
    }
}