using CommunityToolkit.Diagnostics;
using Microsoft.Maui.Layouts;
using MobileUI.Controls.Custom.Scheduling;
using MobileUI.Objects.Interfaces.Scheduling;

namespace MobileUI.Controls.Custom.Scheduling
{
    public class ScheduleHoursView : Grid
    {
        private readonly IScheduleDataSource _dataSource;
        private IList<Frame> _hours = [];
        public ScheduleHoursView(IScheduleDataSource dataSource)
        {
            _dataSource = dataSource;
            InitializeUI();
        }

        public void InitializeUI()
        {
            CreateHoursList();
            AddHoursUI();
        }

        private void AddHoursUI()
        {
            ColumnDefinitions.Add(new ColumnDefinition());
            ColumnDefinitions.Add(new ColumnDefinition());
            ColumnDefinitions.Add(new ColumnDefinition());
            ColumnDefinitions.Add(new ColumnDefinition());

            var totalHours = _hours.Count;

            var amountOfRowsToCreate = (int)Math.Ceiling((double)totalHours / 4);

            var rowIndex = 0;
            var columnIndex = 0;

            // Add the calculated number of row definitions
            for (int i = 0; i < amountOfRowsToCreate; i++) RowDefinitions.Add(new RowDefinition());

            foreach (var hour in _hours)
            {
                // Set the current item in the grid
                Grid.SetRow(hour, rowIndex);
                Grid.SetColumn(hour, columnIndex);

                // Increment the column index
                columnIndex++;

                // If we have filled 4 columns, reset the column index and move to the next row
                if (columnIndex >= 4)
                {
                    columnIndex = 0;
                    rowIndex++;
                }
            }
        }

        private void CreateHoursList()
        {
            Guard.IsNotNull(_dataSource.ScheduleStartDate);
            Guard.IsNotNull(_dataSource.ScheduleEndDate);
            Guard.IsNotNull(_dataSource.UnAvailableHours);

            DateTime startDateTime = _dataSource.ScheduleStartDate;

            while (startDateTime <= _dataSource.ScheduleEndDate)
            {
                // Check if the current startDateTime is scheduled
                var isCurrentDateScheduled = _dataSource.UnAvailableHours
                    .Any(date => date.Hour == startDateTime.Hour && date.Minute == startDateTime.Minute);

                if (!isCurrentDateScheduled)
                {
                    var hour = GenerateLabel(startDateTime);
                    _hours.Add(hour);
                    Children.Add(hour);
                }

                // Add 30 minutes to startDateTime
                startDateTime = startDateTime.AddMinutes(30);
            }
        }

        private static Frame GenerateLabel(DateTime value)
        {
            return new Frame
            {
                Content = new Label
                {
                    Text = $"{value:HH:mm}",
                    BackgroundColor = Colors.WhiteSmoke,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalTextAlignment = TextAlignment.Center
                },
                Margin = 5,
                Padding = 5,
                CornerRadius = 10,
            };
        }
    }

}
