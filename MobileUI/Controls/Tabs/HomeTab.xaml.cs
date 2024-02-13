
using Domain.Common.Service;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Layouts;
using MobileUI.Objects.ViewModels;

namespace MobileUI.Controls.Tabs;

public partial class HomeTab : BasePage
{
    public HomeTab(IApplicationService appService, ILogger<HomeTab> logger):base(appService)
    {
        InitializeComponent();
        BindingContext = new HomeTabViewModel(appService, logger);
    }


    private void ListView_OnItemTapped(object? sender, ItemTappedEventArgs e)
    {
        (BindingContext as HomeTabViewModel)?.ListViewSchedules_OnItemTapped(sender, e);
    }
}