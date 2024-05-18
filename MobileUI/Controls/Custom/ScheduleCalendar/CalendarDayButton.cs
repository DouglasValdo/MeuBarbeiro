using MobileUI.Controls.Custom.Scheduling;
using MobileUI.Objects.Helpers;
using MobileUI.Objects.Interfaces.Scheduling;
using MobileUI.Objects;
using MobileUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Domain.Interfaces.Services;
using Domain.Common.Service;
using Android.Net;

namespace MobileUI.Controls.Custom.ScheduleCalendar
{
    public partial class CalendarDayButton : Frame
    {
        private readonly KeyValuePair<DayOfWeek, DateTime> _myValue;
        private readonly ICalendarServiceProvider? _calendarServiceProvider;
        private readonly Calendar _parent;
        private bool _isHighlighted;
        private bool _isDayOfTheWeek;
        public KeyValuePair<DayOfWeek, DateTime> MyValue { get { return _myValue; } }
        private Label _dayLabel;
        private Label _weekLabel;
        public bool IsHighlighted
        {
            get { return _isHighlighted; }
            set
            {
                _isHighlighted = value;
                SetHighlight();
            }
        }

        public bool IsDayOfTheWeek
        {
            get { return _isDayOfTheWeek; }
            set
            {
                _isDayOfTheWeek = value;

                if (_isDayOfTheWeek) SetDayOfTheWeekHighlight();
            }
        }

        public CalendarDayButton(KeyValuePair<DayOfWeek, DateTime> value, ICalendarServiceProvider? service, Calendar calendarParent)
        {
            _myValue = value;
            _calendarServiceProvider = service;
            AddDayButtonChilds();
            SetTappGesture();
            InitializeUI();
            _parent = calendarParent;
        }

        private void InitializeUI()
        {
            HeightRequest = 60;
            WidthRequest = 45;
            Margin = 2;
            Padding = 0;
            BackgroundColor = Colors.White;
        }

        private void SetTappGesture()
        {
            var tapGesture = new TapGestureRecognizer
            {
                Command = new CommandCalendarDayButton(DayButtonClickAsync, this)
            };

            GestureRecognizers.Add(tapGesture);
        }

        private void AddDayButtonChilds()
        {
            var dayButtonControl = GetLabelDay(_myValue.Value);
            var labelDayOfWeek = GetLabelDayOfWeek(_myValue.Key);

            Content = new VerticalStackLayout
            {
                Children = { dayButtonControl, labelDayOfWeek }
            };
        }

        private Label GetLabelDayOfWeek(DayOfWeek dayOfWeek)
        {
            var label = new Label
            {
                Text = ResourceProvider.PTDaysOfWeek[dayOfWeek],
                HorizontalOptions = LayoutOptions.Center,
                Padding = new Thickness(0, 3, 0, 0),
                TextColor = Colors.Black,
                FontAttributes = FontAttributes.Bold,
                FontSize = 10
            };

            _weekLabel = label;

            return label;
        }

        private Label GetLabelDay(DateTime day)
        {
            var label = new Label
            {
                Text = $"{day.Day}",
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Colors.Black,
                FontSize = 20
            };

            _dayLabel = label;

            return label;
        }

        private async void DayButtonClickAsync(CalendarDayButton? sender)
        {
            if (sender == null || _calendarServiceProvider == null) return;
            //when i click the button to display all available schedule call the barber api get the starting hour and close
            //and all active schedules for the current date provided by the user.
            //with that information display all the available schedule
            //our control that shows all the available schedules will have the hability to call the api to make a schedule.
            //There is a possibility that the user is editing a schedule

            _parent.SetCalendarDayButtonHighlight((CalendarDayButton)sender);

            IBarberShopService? barberAPI = _calendarServiceProvider.GetApplicationService()?.BarberShopService;

            if (barberAPI == null) return;

            var serviceRequest = await ApplicationHelper.ExecuteLoadingTask("Loading available Schedules",
                () => barberAPI.GetBarberShop(Guid.Parse("A8711D93-C79C-4FC4-8980-B45098DDF9FB")));

            if (serviceRequest != null && serviceRequest.IsSuccessfully && serviceRequest.Result != null)
            {
                IScheduleDataSource dataSource = new ScheduleDataSource
                {
                    ScheduleStartDate = serviceRequest.Result.ScheduleStart,
                    ScheduleEndDate = serviceRequest.Result.ScheduleEnd,
                    SelectedDay = ((CalendarDayButton)sender).MyValue.Value,
                    UnAvailableHours = new[] { new DateTime(2024, 5, 17, 13, 0, 0), new DateTime(2024, 5, 17, 13, 30, 0),
                    new DateTime(2024, 5, 17, 9, 0, 0), new DateTime(2024, 5, 17, 16, 30, 0), new DateTime(2024, 5, 17, 17, 0, 0)}
                };

                _parent.DisplayHours(dataSource);
            }
        }

        public void SetDayOfTheWeekHighlight()
        {
            BackgroundColor = Colors.Gray;
        }

        private void SetHighlight()
        {
            if (_isHighlighted)
            {
                BackgroundColor = Colors.Black;
                _dayLabel.TextColor = Colors.White;
                _weekLabel.TextColor = Colors.White;
                BorderColor = Colors.White;
            }
            else
            {
                BackgroundColor = Colors.White;
                _dayLabel.TextColor = Colors.Black;
                _weekLabel.TextColor = Colors.Black;
                BorderColor = Colors.Black;
            }
        }

    }
}
