using Domain.Services.Common;
using Domain.Services.Entities;

namespace Domain.Services.Interfaces;

public interface IScheduleOperations<T>
{
    OperationOutcome Add(ScheduleModel model);
    OperationOutcome<T> GetUserSchedule(Guid userId);
    OperationOutcome Delete(Guid? scheduleId);
    OperationOutcome<IList<T>> GetAll();
    OperationOutcome Update(Guid? scheduleId, ScheduleModel model);
    OperationOutcome<IList<T>> GetAllUserTerminatedSchedules(Guid userId);
}