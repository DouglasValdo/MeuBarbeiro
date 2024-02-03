using Domain.Common.Service;
using Domain.Models.Service;

namespace Domain.Interfaces.Services;

public interface IUserOperations<T>
{
    OperationOutcome<T?> GetByPhoneNumber(int phoneNumber);
    OperationOutcome<T?> GetById(Guid userId);
    OperationOutcome Add(UserModel model);
    OperationOutcome Delete(Guid? userId);
    OperationOutcome<IList<T>> GetAll();
    OperationOutcome Update(Guid? userId, UserModel model);
}