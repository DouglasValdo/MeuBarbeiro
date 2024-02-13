using Domain.Common.Service;
using Domain.Entities;
using Domain.Models.Service;

namespace Domain.Interfaces.Services;

public interface IUserService
{
    public Task<OperationOutcome<User?>?> GetUserByPhoneNumberAsync(string phoneNumber);
    public Task<OperationOutcome<User?>?> GetUserByIdAsync(Guid userId);
    public Task<OperationOutcome?> Register(UserModel user);
}