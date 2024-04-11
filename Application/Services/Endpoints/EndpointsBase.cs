using Domain.Interfaces;

namespace ApplicationStructure.Services.Endpoints;

public class EndpointsBase
{
    protected readonly HttpClient Client;

    protected EndpointsBase(HttpClient client)
    {
        Client = client;
    }
}