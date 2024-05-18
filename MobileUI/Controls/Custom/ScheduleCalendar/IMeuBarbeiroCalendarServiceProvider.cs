using Domain.Common.Service;

namespace MobileUI.Controls.Custom.ScheduleCalendar
{
    public interface ICalendarServiceProvider
    {
        IApplicationService? GetApplicationService();
    }
}