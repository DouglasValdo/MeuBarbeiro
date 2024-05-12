using CommunityToolkit.Diagnostics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileUI.Controls.Custom.Scheduling
{
    public class CalendarConfigurationProvider
    {
        private DateTime _startDateTime;
        public List<KeyValuePair<DayOfWeek, DateTime>> DaysToSchedule
        {
            get
            {
                return GetDaysToSchedule();
            }
        }

        public CalendarConfigurationProvider(DateTime startDateTime)
        {
            _startDateTime = startDateTime;
        }


        private List<KeyValuePair<DayOfWeek, DateTime>> GetDaysToSchedule()
        {
            Guard.IsNotNull(_startDateTime, nameof(_startDateTime));

            var days = new List<KeyValuePair<DayOfWeek, DateTime>>
            {
                GetWeekDay(DayOfWeek.Monday),
                GetWeekDay(DayOfWeek.Tuesday),
                GetWeekDay(DayOfWeek.Wednesday),
                GetWeekDay(DayOfWeek.Thursday),
                GetWeekDay(DayOfWeek.Friday),
                GetWeekDay(DayOfWeek.Saturday),
                GetWeekDay(DayOfWeek.Sunday)
            };

            return days;
        }

        private KeyValuePair<DayOfWeek, DateTime> GetWeekDay(DayOfWeek dayOfWeek)
        {
            var calendar = CultureInfo.InvariantCulture.Calendar;
            var currentDayOfWeek = calendar.GetDayOfWeek(_startDateTime);

            if (_dayOfWeekIndex[dayOfWeek] > _dayOfWeekIndex[currentDayOfWeek])
                _startDateTime = _startDateTime.AddDays((_dayOfWeekIndex[dayOfWeek] - _dayOfWeekIndex[currentDayOfWeek]));
            else if (_dayOfWeekIndex[dayOfWeek] < _dayOfWeekIndex[currentDayOfWeek])
                _startDateTime = _startDateTime.AddDays(-(_dayOfWeekIndex[currentDayOfWeek] - _dayOfWeekIndex[dayOfWeek]));


            return new KeyValuePair<DayOfWeek, DateTime>(dayOfWeek, _startDateTime);
        }

        private static readonly Dictionary<DayOfWeek, int> _dayOfWeekIndex = new()
        {
            {DayOfWeek.Monday, 1 },
            {DayOfWeek.Tuesday, 2 },
            {DayOfWeek.Wednesday, 3 },
            {DayOfWeek.Thursday, 4 },
            {DayOfWeek.Friday, 5 },
            {DayOfWeek.Saturday, 6 },
            {DayOfWeek.Sunday, 7 },
        };
    }
}
