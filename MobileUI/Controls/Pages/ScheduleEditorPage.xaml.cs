using Domain.Common.Service;

namespace MobileUI.Controls.Pages;

public partial class ScheduleEditorPage : BasePage
{
    public ScheduleEditorPage(IApplicationService service) : base(service)
    {
        InitializeComponent();
    }
}