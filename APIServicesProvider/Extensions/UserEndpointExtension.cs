﻿using Application.DBContext;
using Application.Repositories;
using Application.Services.Operations;
using Domain.Entities;
using Domain.Repository;
using Domain.Services.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ServicesProvider.Extensions;

public static class UserEndpointExtension
{
    public static IEndpointRouteBuilder
        MapUserEndpoint(this WebApplication app, MeuBarbeiroDbContext dbContext)
    {
        IRepository<User> userRepository = new UserRepository(dbContext);

        var userEndPointOperations = new UserServiceOperations(userRepository);

        var userGroupedEndpoint = app.MapGroup("/api/User").WithTags("User");

        userGroupedEndpoint.MapPost("/AddUser/{model}", ([FromBody] UserModel model)
            => userEndPointOperations.Add(model));
        
        userGroupedEndpoint.MapPut("/UpdateUser/{userId:guid}/{model}", (Guid userId, [FromBody] UserModel model)
            => userEndPointOperations.Update(userId, model));
        
        userGroupedEndpoint.MapDelete("/DeleteUser/{userId:guid}", (Guid userId)
            => userEndPointOperations.Delete(userId));

        userGroupedEndpoint.MapGet("/GetUserByPhoneNumber/{phoneNumber:int}", (int phoneNumber)
            => userEndPointOperations.GetByPhoneNumber(phoneNumber));

        userGroupedEndpoint.MapGet("/GetUserById/{userId:guid}", (Guid userId)
            => userEndPointOperations.GetById(userId));
        
        userGroupedEndpoint.MapGet("/GetUsers", ()
            => userEndPointOperations.GetAll());

        return app;
    }
}