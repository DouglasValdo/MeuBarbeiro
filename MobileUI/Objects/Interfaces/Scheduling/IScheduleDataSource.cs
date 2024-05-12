using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileUI.Objects.Interfaces.Scheduling
{
    public interface IScheduleDataSource
    {
        DateTime ScheduleStartDate { get; set; }
        DateTime ScheduleEndDate { get; set; }
        DateTime SelectedDay { get; set; }
        IList<DateTime> UnAvailableHours { get; set; }
    }
}
