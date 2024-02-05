using Domain.Common.Service;
using Domain.Models.Service;

namespace Domain.Interfaces.Services;

public interface IScheduleOperations<T>
{
    OperationOutcome Add(ScheduleModel model);
    OperationOutcome<List<T>>? GetUserSchedule(Guid userId);
    OperationOutcome Delete(Guid? scheduleId);
    OperationOutcome<IList<T>> GetAll();
    OperationOutcome Update(Guid? scheduleId, ScheduleModel model);
    OperationOutcome<IList<T>> GetAllUserTerminatedSchedules(Guid userId);
}