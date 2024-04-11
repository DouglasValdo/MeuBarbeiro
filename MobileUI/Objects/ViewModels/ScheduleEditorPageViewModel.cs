using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Common.Service;
using Microsoft.Extensions.Logging;
using UraniumUI.Material.Attachments;

namespace MobileUI.Objects.ViewModels;

public partial class ScheduleEditorPageViewModel: ViewModelBase
{
    [ObservableProperty] private BottomSheetView _bottomSheetView = new ();
    [ObservableProperty] private IView _container;

    public ScheduleEditorPageViewModel(IApplicationService appServiceProvider, ILogger logger)
        : base(appServiceProvider, logger)
    {
        _container = GetContentView();
    }

    private IView GetContentView()
    {
        var horizontalLayout = new HorizontalStackLayout();
        
        
        
        
        return horizontalLayout;
    }

}