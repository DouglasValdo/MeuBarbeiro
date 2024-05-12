using MobileUI.Controls.Custom.Scheduling;
using MobileUI.Objects.Interfaces.Scheduling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileUI.Objects
{
    public class ScheduleDataSource : IScheduleDataSource
    {
        public DateTime ScheduleStartDate { get ; set; }
        public DateTime ScheduleEndDate { get ; set; }
        public DateTime SelectedDay { get ; set; }
        public IList<DateTime> UnAvailableHours { get ; set; } = [];
    }
}
