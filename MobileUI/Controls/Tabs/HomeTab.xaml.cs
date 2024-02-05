using Domain.Common.Service;
using Microsoft.Extensions.Logging;
using MobileUI.Objects.ViewModels;

namespace MobileUI.Controls.Tabs;

public partial class HomeTab : BasePage
{
    public HomeTab(IApplicationService appService, ILogger<HomeTab> logger):base(appService)
    {
        InitializeComponent();
        BindingContext = new HomeTabViewModel(appService, logger);
    }
}