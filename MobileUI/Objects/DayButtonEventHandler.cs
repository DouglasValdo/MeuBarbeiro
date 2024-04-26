using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileUI.Objects
{
    public delegate void SchedulingCalendarDayButtonClick(object sender, SchedulingCalendarDayButtonArgs e);

    public class SchedulingCalendarDayButtonArgs : EventArgs
    {
        public DateTime Valuue { get; set; }
    }
}
