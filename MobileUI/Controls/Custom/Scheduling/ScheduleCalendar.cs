using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using Domain.Common.Service;
using Domain.Entities;
using Domain.Interfaces.Services;
using MobileUI.Controls.Custom.Scheduling;
using MobileUI.Objects;
using MobileUI.Objects.Helpers;
using MobileUI.Objects.Interfaces.Scheduling;
using MobileUI.Objects.ViewModels;
using MobileUI.Utils;

namespace MobileUI.Controls.Custom.Scheduling
{
    public partial class ScheduleCalendar : VerticalStackLayout
    {
        private readonly CalendarConfigurationProvider _configurationProvider = new(DateTime.Now);
        private List<ScheduleCalendarDayButton> _buttons = [];
        public ScheduleCalendar()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            BackgroundColor = Colors.White;
            Padding = 5;
            HorizontalOptions = LayoutOptions.Center;

            DrawCalendarWeekUI();
        }


        private void DrawCalendarWeekUI()
        {
            DrawMonthUI();
            DrawControlContent();
        }

        private void DrawControlContent()
        {
            var columnIndex = 0;

            var container = new HorizontalStackLayout
            {
                HorizontalOptions = LayoutOptions.Fill,
                Margin = 2
            };

            Children.Add(container);

            foreach (var item in _configurationProvider.DaysToSchedule)
            {
                var isHighlighted = item.Value.Day == DateTime.Now.Day;

                var frameContainer      = GetFrameContainer(item.Value, isHighlighted);
                var dayButtonControl    = GetLabelDay(item.Value, isHighlighted);
                var labelDayOfWeek      = GetLabelDayOfWeek(item.Key, isHighlighted);

                frameContainer.Content = new VerticalStackLayout
                {
                    Children = { dayButtonControl, labelDayOfWeek }
                };

                container.Add(frameContainer);

                _buttons.Add(frameContainer);

                columnIndex++;
            }

        }

        private ScheduleCalendarDayButton GetFrameContainer(DateTime value, bool isHighlighted)
        {
            var frame = new ScheduleCalendarDayButton(value)
            {
                CornerRadius = 5,
                Margin = 2,
                Padding = 2,
                BackgroundColor = Colors.Black,
                WidthRequest = 50,
                HeightRequest = 50
            };

            var tapGesture = new TapGestureRecognizer();
            
            tapGesture.Tapped += DayButtonClickAsync;

            frame.GestureRecognizers.Add(tapGesture);

            if (isHighlighted)//try also execute the tap gesture to allways show available schedules for datetime.now
                frame.BackgroundColor = Colors.WhiteSmoke;

            return frame;
        }

        private void DrawMonthUI()
        {
            var currentMonth = new Label
            {
                Text = $"{DateTime.Now:MMMM, yyyy}",
                FontSize = 20,
                Padding = new Thickness(0, 10),
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Bold,
            };

            Children.Add(currentMonth);
        }

        private static Label GetLabelDayOfWeek(DayOfWeek dayOfWeek, bool isHighlighted)
        {
            var label = new Label
            {
                Text = ResourceProvider.PTDaysOfWeek[dayOfWeek],
                HorizontalOptions = LayoutOptions.Center,
                Padding = new Thickness(0, 3,0, 0),
                TextColor = Colors.White,
                FontAttributes = FontAttributes.Bold,
                FontSize = 10
            };

            if (isHighlighted)
                label.TextColor = Colors.Black;

            return label;
        }

        private static Label GetLabelDay(DateTime day, bool isHighlighted)
        {
            var label = new Label
            {
                Text = $"{day.Day}",
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Colors.White,
                FontSize = 20
            };

            if(isHighlighted)
                label.TextColor = Colors.Black;

            return label;
        }

        protected async void DayButtonClickAsync(object? sender, TappedEventArgs args)
        {

            if(sender == null) return;
            //when i click the button to display all available schedule cal the barber api get the starting hour and close
            //and all active schedules for the current date provided by the user.
            //with that information display all the available schedule
            //our control that shows all the available schedules will have the hability to call the api to make a schedule.
            //There is a possibility that the user is editing a schedule

            var serviceRequest = await ApplicationHelper.ExecuteLoadingTask("Loading available Schedules",
                () => SchedulingService.GetBarberShop(Guid.Parse("A8711D93-C79C-4FC4-8980-B45098DDF9FB")));

            if (serviceRequest != null && serviceRequest.IsSuccessfully && serviceRequest.Result != null)
            {
                IScheduleDataSource dataSource = new ScheduleDataSource
                {
                    ScheduleStartDate = serviceRequest.Result.ScheduleStart,
                    ScheduleEndDate = serviceRequest.Result.ScheduleEnd,
                    SelectedDay = ((ScheduleCalendarDayButton)sender).MyDateValue,
                    UnAvailableHours = new[] { DateTime.Now, DateTime.Now.AddMinutes(30), }
                };

                ScheduleHoursContainer.IsPresented = true;

                ScheduleHoursContainer.Content = new ScheduleHoursView(dataSource);
            }
        }
    }

}
