using Domain.Interfaces.Services;

namespace Domain.Common.Service;

public interface IApplicationService
{
     IScheduleService ScheduleService { get; }
     ITaskService TaskService     { get;  }
     IUserService UserService         { get; }
}