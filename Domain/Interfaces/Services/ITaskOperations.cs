using Domain.Common.Service;
using Domain.Models.Service;

namespace Domain.Interfaces.Services;

public interface ITaskOperations<T>
{
    OperationOutcome Add(TaskModel model);
    OperationOutcome Delete(Guid? taskTypeId);
    OperationOutcome<IList<T>> GetAll();
    OperationOutcome Update(Guid? taskTypeId, TaskModel model);
}