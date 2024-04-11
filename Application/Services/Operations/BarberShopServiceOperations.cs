using Domain.Common.Service;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Repository;
// ReSharper disable All

namespace ApplicationStructure.Services.Operations;

public class BarberShopServiceOperations:BaseOperations<Barber>, IBarberShopOperations<Barber>
{
    public BarberShopServiceOperations(IRepository<Barber> repository):base(repository) 
    {
        
    }
    public OperationOutcome<Barber?> GetBarberShop(Guid barberShopId)
    {
        try
        {
            var barberShop = MyRepository
                .Get((s) => s.Id == barberShopId && s.IsDeleted == false);
            
            return new OperationOutcome<Barber?> { IsSuccessfully = true, Result = barberShop};
        }
        catch (Exception e)
        {
            return new OperationOutcome<Barber?>
            {
                IsSuccessfully = false,
                ErrorMessage = e.Message
            };
        }
    }
}