using Domain.Common.Service;
using Domain.Entities;

namespace Domain.Interfaces.Services;

public interface IBarberShopService
{
    Task<OperationOutcome<Barber?>?> GetBarberShop(Guid barberShopId);
}