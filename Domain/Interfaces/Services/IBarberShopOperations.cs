using Domain.Common.Service;

namespace Domain.Interfaces.Services;

public interface IBarberShopOperations<T>
{
    OperationOutcome<T?> GetBarberShop(Guid barberShopId);
}