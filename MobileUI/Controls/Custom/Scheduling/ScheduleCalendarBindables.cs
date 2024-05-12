using CommunityToolkit.Diagnostics;
using Domain.Interfaces.Services;
using UraniumUI.Material.Attachments;

namespace MobileUI.Controls.Custom.Scheduling
{
    public partial class ScheduleCalendar
    {
        public static readonly BindableProperty ScheduleHoursContainerProperty = BindableProperty
            .Create(nameof(ScheduleHoursContainer), typeof(BottomSheetView), typeof(ScheduleCalendar), null, BindingMode.TwoWay);

        public BottomSheetView ScheduleHoursContainer
        {
            get
            {
                return (BottomSheetView)GetValue(ScheduleHoursContainerProperty);
            }

            set
            {
                SetValue(ScheduleHoursContainerProperty, value);
            }
        }

        public static readonly BindableProperty SchedulingServiceProperty = BindableProperty
            .Create(nameof(SchedulingService), typeof(IBarberShopService), typeof(ScheduleCalendar), null, BindingMode.TwoWay);

        public IBarberShopService SchedulingService
        {
            get => (IBarberShopService)GetValue(SchedulingServiceProperty);
            set => SetValue(SchedulingServiceProperty, value);
        }

    }
}
