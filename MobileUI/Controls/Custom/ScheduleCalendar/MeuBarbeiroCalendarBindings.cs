using Domain.Common.Service;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileUI.Controls.Custom.ScheduleCalendar
{
    public partial class MeuBarbeiroCalendar
    {
        public static readonly BindableProperty AppServiceProperty = BindableProperty
            .Create(nameof(AppService), typeof(IApplicationService), typeof(MeuBarbeiroCalendar), null, BindingMode.TwoWay);

        public IApplicationService? AppService
        {
            get => (IApplicationService?)GetValue(AppServiceProperty);
            set => SetValue(AppServiceProperty, value);
        }
    }
}
