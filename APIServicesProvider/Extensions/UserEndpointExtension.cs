using ApplicationStructure.DBContext;
using ApplicationStructure.Repositories;
using ApplicationStructure.Services.Operations;
using Domain.Entities;
using Domain.Models.Service;
using Domain.Repository;
using Infrastructure.DBProvider;
using Microsoft.AspNetCore.Mvc;

namespace ServicesProvider.Extensions;

public static class UserEndpointExtension
{
    public static IEndpointRouteBuilder
        MapUserEndpoint(this WebApplication app)
    {
        MeuBarbeiroDbContext dbContext = app.Services.GetRequiredService<MeuBarbeiroDbContext>();

        IRepository<User> userRepository = new UserRepository(dbContext);

        var userEndPointOperations = new UserServiceOperations(userRepository);

        var userGroupedEndpoint = app.MapGroup("/api/User").WithTags("User");

        userGroupedEndpoint.MapPost("/AddUser", ([FromBody] UserModel model)
            => userEndPointOperations.Add(model));

        userGroupedEndpoint.MapPut("/UpdateUser/{userId:guid}", (Guid userId, [FromBody] UserModel model)
            => userEndPointOperations.Update(userId, model));

        userGroupedEndpoint.MapDelete("/DeleteUser/{userId:guid}", (Guid userId)
            => userEndPointOperations.Delete(userId)).CacheOutput(p => p.NoCache());

        userGroupedEndpoint.MapGet("/GetUserByPhoneNumber/{phoneNumber:int}", (int phoneNumber)
            => userEndPointOperations.GetByPhoneNumber(phoneNumber));

        userGroupedEndpoint.MapGet("/GetUserById/{userId:guid}", (Guid userId)
            => userEndPointOperations.GetById(userId));

        userGroupedEndpoint.MapGet("/GetUsers", ()
            => userEndPointOperations.GetAll());

        return app;
    }
}