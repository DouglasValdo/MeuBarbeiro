using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileUI.Controls.Custom.ScheduleCalendar
{
    public class CommandCalendarDayButton(Action<CalendarDayButton?> execute, CalendarDayButton? dayButton) : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly Action<CalendarDayButton?> _execute = execute;

        public bool CanExecute(object? parameter)
        {
            return _execute != null && dayButton != null;
        }

        public void Execute(object? parameter)
        {
            _execute.Invoke(dayButton);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
