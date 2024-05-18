using CommunityToolkit.Diagnostics;
using Domain.Common.Service;
using Microsoft.Maui.Layouts;
using MobileUI.Controls.Custom.Scheduling;
using MobileUI.Objects.Interfaces.Scheduling;
using MobileUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileUI.Controls.Custom.ScheduleCalendar
{
    public partial class Calendar : VerticalStackLayout
    {
        private DateTime _selectedDateTime = new DateTime(2024, 5, 16, 16, 30, 0);
        private readonly DateTime _startDateTime;
        private readonly CalendarConfigurationProvider _configuration;
        private readonly ICalendarServiceProvider? _calendarServiceProvider;
        private VerticalStackLayout _hoursContainer = GetHoursContainer();
        public DateTime StartDateTime { get { return _startDateTime; } }
        private IList<CalendarDayButton> _calendarDayButtons = [];

        public DateTime SelectedDateTime { get { return _selectedDateTime; } set { _selectedDateTime = value; } }

        public Calendar(DateTime startDateTime, ICalendarServiceProvider? service)
        {
            Guard.IsNotNull(startDateTime);
            _startDateTime = startDateTime;
            _configuration = new(_startDateTime);
            _calendarServiceProvider = service;
        }

        public void Show()
        {
            InitializeUI();
            AddCalendarMonthUI();
            AddCalendarDaysUI();
            AddCalendarHoursUI();
            HighlightSelectedDateTime();
        }

        private void InitializeUI()
        {
            VerticalOptions = LayoutOptions.Fill;
            HorizontalOptions = LayoutOptions.Fill;
        }

        private void AddCalendarMonthUI()
        {
            var currentMonth = new Label
            {
                Text = $"{_startDateTime:MMMM, yyyy}",
                FontSize = 20,
                Padding = new Thickness(0, 10),
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Bold,
            };

            Children.Add(currentMonth);
        }

        private void AddCalendarDaysUI()
        {
            var container = new FlexLayout
            {
                HorizontalOptions = LayoutOptions.Fill,
                Margin = 2,
                AlignContent = FlexAlignContent.SpaceEvenly
            };

            Children.Add(container);

            AddDaysOfWeekUI(container);
        }

        private void AddDaysOfWeekUI(FlexLayout container)
        {
            foreach (var item in _configuration.DaysToSchedule)
            {
                var isCurrentDayOfWeek = item.Value.Day == DateTime.Now.Day;

                var dayButton = GetCalendarDayButton(item, isCurrentDayOfWeek);

                container.Add(dayButton);

                _calendarDayButtons.Add(dayButton);
            }
        }

        private CalendarDayButton GetCalendarDayButton(KeyValuePair<DayOfWeek, DateTime> value, bool isCurrentDayOfWeek)
        {
            var dayButton = new CalendarDayButton(value, _calendarServiceProvider, this)
            {
                IsDayOfTheWeek = isCurrentDayOfWeek,
            };

            return dayButton;
        }

        private void AddCalendarHoursUI()
        {
            var container = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Margin = 2,
            };
            container.Children.Add(_hoursContainer);
            Children.Add(container);
        }

        private static VerticalStackLayout GetHoursContainer()
        {
            var hoursContainer = new VerticalStackLayout
            {
                BackgroundColor = Colors.Transparent,
                HorizontalOptions = LayoutOptions.Fill
            };

            return hoursContainer;
        }

        internal void DisplayHours(IScheduleDataSource dataSource)
        {
            var hoursView = new ScheduleHoursView(dataSource);

            _hoursContainer.Children.Clear();
            _hoursContainer.Children.Add(hoursView);
        }

        public void SetCalendarDayButtonHighlight(CalendarDayButton highlight)
        {
            if (!_calendarDayButtons.Any()) return;

            foreach (var button in _calendarDayButtons)
                button.IsHighlighted = false;

            var buttonToHighlight = _calendarDayButtons.FirstOrDefault(button => button.Id == highlight.Id);

            if (buttonToHighlight != null) buttonToHighlight.IsHighlighted = true;
        }
        private void HighlightSelectedDateTime()
        {
            var selectedDateTimeButton = _calendarDayButtons.FirstOrDefault(d => d.MyValue.Value.Day == _selectedDateTime.Day);

            if(selectedDateTimeButton != null)
            {
                selectedDateTimeButton.IsHighlighted = true;

                if (selectedDateTimeButton.GestureRecognizers.FirstOrDefault() is not TapGestureRecognizer gesture) return;

                //This ensures the gesture’s Tapped event is invoked on the main thread, which is required for UI operations.
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    if(gesture.Command != null && gesture.Command.CanExecute(null))
                        gesture.Command?.Execute(null);
                });                
            }
        }
    }
}
