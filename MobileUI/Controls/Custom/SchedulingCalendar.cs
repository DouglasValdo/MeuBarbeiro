using CommunityToolkit.Diagnostics;
using Domain.Interfaces.Services;
using MobileUI.Objects.ViewModels;
using MobileUI.Utils;
using System.Collections.ObjectModel;

namespace MobileUI.Controls.Custom
{
    public class SchedulingCalendar : Grid
    {
        private GridLength _rowHeight = new (40);
        public Label MonthLabel { get; set; } = new Label();
        //ObservableCollection<SchedulingCalendarDayButton> _myDaysButton = new();
        private readonly SchedulingCalendarViewModel _schedulingCalendarViewModel;
        private IScheduleService? _schedulingService;
        public IScheduleService? SchedulingService
        {
            get
            {
                Guard.IsNotNull(SchedulingService, nameof(SchedulingService));
                return _schedulingService;
            }
            set { _schedulingService = value; }
        }

        private DateTime _startDate = DateTime.Now;
        public DateTime CalendarStartDateTime
        {
            get { return _startDate; }
            set
            {
                Guard.IsNotNull(value, nameof(CalendarStartDateTime));
                _startDate = value;
                _schedulingCalendarViewModel.CalendarStartDate = value;
            }
        }

        public SchedulingCalendar()
        {
            _schedulingCalendarViewModel = new SchedulingCalendarViewModel();
            _schedulingCalendarViewModel.CalendarStartDate = _startDate;
            InitializeUI();
        }

        private void InitializeUI()
        {
            DefineRows();
            DefineColumns();
            DefineCalendarWeekUI();
        }

        private void DefineRows()
        {
            //row with the information of month and year
            RowDefinitions.Add(new RowDefinition { Height = _rowHeight});
            //row with the weekname
            RowDefinitions.Add(new RowDefinition { Height = _rowHeight });
            //row with the day of the month
            RowDefinitions.Add(new RowDefinition { Height = _rowHeight });
        }

        private void DefineColumns()
        {
            //add column for each day of week
            for (var p = 1; p <= 7; p++) ColumnDefinitions.Add(new ColumnDefinition());
        }

        private void DefineCalendarWeekUI()
        {
            SetMonthUI();
            SetWeek();
            SetDayButtonsUI();

        }

        private void SetMonthUI()
        {
            MonthLabel = new Label
            {
                Text = $"{_startDate:MMMM, yyyy}",
                FontSize = 20
            };

            Children.Add(MonthLabel);
            Grid.SetRow(MonthLabel, 0);
            Grid.SetColumnSpan(MonthLabel, ColumnDefinitions.Count);
        }

        private void SetWeek()
        {
            var columnIndex = 0;

            foreach (var item in _schedulingCalendarViewModel.DaysToSchedule)
            {
                var label = new Label { Text = ResourceProvider.PTDaysOfWeek[item.Key], HorizontalOptions = LayoutOptions.Center };
                Children.Add(label);
                Grid.SetRow(label, 1);
                Grid.SetColumn(label, columnIndex);

                columnIndex++;
            }
        }

        private void SetDayButtonsUI()
        {
            var columnIndex = 0;

            foreach (var item in _schedulingCalendarViewModel.DaysToSchedule)
            {
                var dayButton = new SchedulingCalendarDayButton(item.Value)
                {
                    HorizontalOptions = LayoutOptions.Center
                };
                Children.Add(dayButton);
                Grid.SetRow(dayButton, 2);
                Grid.SetColumn(dayButton, columnIndex);

                columnIndex++;
            }
        }
    }
}
