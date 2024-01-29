using Domain.Services.Common;
using Domain.Services.Entities;

namespace Domain.Services.Interfaces;

public interface ITaskOperations<T>
{
    OperationOutcome Add(TaskModel model);
    OperationOutcome Delete(Guid? taskTypeId);
    OperationOutcome<IList<T>> GetAll();
    OperationOutcome Update(Guid? taskTypeId, TaskModel model);
}