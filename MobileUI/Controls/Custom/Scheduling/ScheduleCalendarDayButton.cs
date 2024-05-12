namespace MobileUI.Controls.Custom.Scheduling
{
    public class ScheduleCalendarDayButton : Frame
    {
        public DateTime MyDateValue { get; }
        public EventHandler<SchedulingCalendarDayButtonArgs>? SchedulingCalendarDayButtonClick;

        public ScheduleCalendarDayButton(DateTime dateTime)
        {
            MyDateValue = dateTime;
        }

    }
}
