using Domain.Common.Service;
using Domain.Interfaces.Services;
using MobileUI.Controls.Custom.Scheduling;
using MobileUI.Objects.Helpers;
using MobileUI.Objects.Interfaces.Scheduling;
using MobileUI.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileUI.Controls.Custom.ScheduleCalendar
{
    public partial class MeuBarbeiroCalendar : Frame, ICalendarServiceProvider
    {
        private readonly Calendar _myCalendar;

        public MeuBarbeiroCalendar()
        {
            InitializeUI();
            _myCalendar = new(DateTime.Now, this);

            Content = _myCalendar;

            _myCalendar.Show();
        }

        private void InitializeUI()
        {
            HorizontalOptions = LayoutOptions.Fill;
            VerticalOptions = LayoutOptions.Fill;
            Margin = 0;
            Padding = 5;
        }

        public IApplicationService? GetApplicationService()
        {
            return AppService;
        }
    }
}
