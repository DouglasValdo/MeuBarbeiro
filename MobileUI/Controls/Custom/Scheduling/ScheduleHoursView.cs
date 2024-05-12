using CommunityToolkit.Diagnostics;
using MobileUI.Controls.Custom.Scheduling;
using MobileUI.Objects.Interfaces.Scheduling;

namespace MobileUI.Controls.Custom.Scheduling
{
    public class ScheduleHoursView : FlexLayout
    {
        private readonly IScheduleDataSource _dataSource;
        public ScheduleHoursView(IScheduleDataSource dataSource)
        {
            _dataSource = dataSource;
            InitializeUI();
        }

        public void InitializeUI()
        {
            GenerateItems();
        }

        private void GenerateItems()
        {
            //this code is not correct check if when is necessary to use
            Guard.IsNotNull(_dataSource);

            DateTime startDateTime = _dataSource.ScheduleStartDate;

            while(startDateTime <= _dataSource.ScheduleEndDate)
            {
                var possibleAvailableHour = startDateTime.AddMinutes(30);

                if (_dataSource.UnAvailableHours.Contains(possibleAvailableHour)) continue;

                Children.Add(new Label { Text = $"{possibleAvailableHour:HH:mm}" });

                startDateTime = possibleAvailableHour;
            }
        }
    }
}
