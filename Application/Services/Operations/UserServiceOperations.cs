using Domain.Common.Service;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Service;
using Domain.Repository;

namespace ApplicationStructure.Services.Operations;

public class UserServiceOperations(IRepository<User> repository)
    : BaseOperations<User>(repository), IUserOperations<User>
{
    public OperationOutcome<User?> GetByPhoneNumber(int phoneNumber)
    {
        try
        {
            var foundUser = MyRepository?.Get((u) => u.PhoneNumber == phoneNumber);
            
            return new OperationOutcome<User?>
            {
                IsSuccessfully = true,
                Result = foundUser
            };
        }
        catch (Exception e)
        {
            return new OperationOutcome<User?> { IsSuccessfully = false, ErrorMessage = e.Message };
        }
    }

    public OperationOutcome<User?> GetById(Guid userId)
    {
        try
        {
            var foundUser = MyRepository?.Get((u) => u.Id == userId);
            
            return new OperationOutcome<User?>
            {
                IsSuccessfully = true,
                Result = foundUser
            };
        }
        catch (Exception e)
        {
            return new OperationOutcome<User?> { IsSuccessfully = false, ErrorMessage = e.Message };
        }
    }

    public OperationOutcome Add(UserModel model)
    {
        try
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                IsDeleted = model.IsDeleted,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber
            };
            
            MyRepository?.Add(newUser);

            return new OperationOutcome
            {
                IsSuccessfully = true
            };
        }
        catch (Exception e)
        {
            return new OperationOutcome { IsSuccessfully = false, ErrorMessage = e.Message };
        }
    }

    public OperationOutcome Delete(Guid? userId)
    {
        try
        {
            MyRepository?.Remove((u) => u.Id == userId);
            
            return new OperationOutcome { IsSuccessfully = true };
        }
        catch (Exception e)
        {
            return new OperationOutcome { IsSuccessfully = false, ErrorMessage = e.Message };
        }
    }

    public OperationOutcome<IList<User>> GetAll()
    {
        try
        {
            var allUsers = MyRepository?.GetAll();
            
            return new OperationOutcome<IList<User>> { IsSuccessfully = true, Result = allUsers};
        }
        catch (Exception e)
        {
            return new OperationOutcome<IList<User>> { IsSuccessfully = false, ErrorMessage = e.Message };
        }
    }

    public OperationOutcome Update(Guid? userId, UserModel model)
    {
        throw new NotImplementedException();
    }
}