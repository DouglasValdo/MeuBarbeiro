using ApplicationStructure.Extensions;
using Domain.Common.Service;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace ApplicationStructure.Services.Endpoints;

public class BarberShopEndpoint: EndpointsBase, IBarberShopService
{
    private const string ENDPOINT = "Barber/";
    
    public BarberShopEndpoint(HttpClient client) : base(client)
    {
    }

    public async Task<OperationOutcome<Barber?>?> GetBarberShop(Guid barberShopId)
    {
        var responseMessage = await Client.GetAsync($"{ENDPOINT}GetBarberShop/{barberShopId}");
        
        var result    = await responseMessage.Content.GetFromJsonAsync<OperationOutcome<Barber?>>();
        
        return result;
    }
}