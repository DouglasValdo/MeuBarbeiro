using MobileUI.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileUI.Controls.Custom
{
    public class SchedulingCalendarDayButton : Button
    {
        public DateTime MyDateValue {  get; }
        public SchedulingCalendarDayButtonClick? SchedulingCalendarDayButtonClick;

        public SchedulingCalendarDayButton(DateTime dateTime)
        {
            MyDateValue = dateTime;
            Text = $"{MyDateValue.Day}";
        }
    }
}
