using Domain.Common.Service;
using Microsoft.Extensions.Logging;
using MobileUI.Objects;
using MobileUI.Objects.ViewModels;
using UraniumUI.Material.Attachments;

namespace MobileUI.Controls.Pages;

public partial class ScheduleEditorPage : BasePage
{
    public ScheduleEditorPage(IApplicationService service, ILogger<ScheduleEditorPage> logger) : base(service)
    {
        InitializeComponent();
        BindingContext = new ScheduleEditorPageViewModel(service, logger);

    }
}