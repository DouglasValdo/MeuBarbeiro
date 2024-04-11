using Domain.Common.Service;
using Microsoft.Extensions.Logging;

namespace MobileUI.Objects.Helpers;

public class ScheduleEditorHelper(IApplicationService service, ILogger logger, Guid currentUserBarberShopId)
{
    // public async ReadOnlySpan<Barber> GetBarberShopDaySchedules(DateTime day)
    // {
    //     var barberSchedules = await service.BarberShopService
    //         .GetBarberShop(currentUserBarberShopId);
    //     
    //     
    // }
}