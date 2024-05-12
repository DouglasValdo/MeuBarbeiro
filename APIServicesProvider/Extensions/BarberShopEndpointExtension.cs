﻿using ApplicationStructure.DBContext;
using ApplicationStructure.Repositories;
using ApplicationStructure.Services.Operations;
using Domain.Entities;
using Domain.Repository;
using Infrastructure.DBProvider;
using Microsoft.EntityFrameworkCore;

namespace ServicesProvider.Extensions;

public static class BarberShopEndpointExtension
{
    public static IEndpointRouteBuilder MapBarberShopEndpoint
        (this WebApplication app)
    {
        MeuBarbeiroDbContext dbContext = app.Services.GetRequiredService<MeuBarbeiroDbContext>();
        IRepository<Barber> barberShopRepository = new BarberShopRepository(dbContext);

        var barberShopServiceOperations = new BarberShopServiceOperations(barberShopRepository);

        var taskGroupedEndpoint = app.MapGroup("/api/Barber").WithTags("Barber");

        taskGroupedEndpoint.MapGet("/GetBarberShop/{barberShopId:guid}",
            (Guid barberShopId)
                => barberShopServiceOperations.GetBarberShop(barberShopId));

        return app;
    }
}