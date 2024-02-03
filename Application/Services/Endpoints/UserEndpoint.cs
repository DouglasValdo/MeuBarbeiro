using ApplicationStructure.Extensions;
using Domain.Common.Service;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace ApplicationStructure.Services.Endpoints;

public class UserEndpoint : EndpointsBase, IUserService
{
    private const string ENDPOINT = "User/";
    
    public UserEndpoint(HttpClient client) : base(client)
    {
    }
    public async Task<OperationOutcome<User?>?> GetUserByPhoneNumberAsync(string phoneNumber)
    {
        var responseMessage = await Client.GetAsync($"{ENDPOINT}GetUserByPhoneNumber/{phoneNumber}");
        
        var result    = await responseMessage.Content.GetFromJsonAsync<OperationOutcome<User?>>();
        
        return result;
    }

    public async Task<OperationOutcome<User?>?> GetUserByIdAsync(Guid userId)
    {
        var responseMessage = await Client.GetAsync($"{ENDPOINT}GetUserById/{userId}");
        
        var result    = await responseMessage.Content.GetFromJsonAsync<OperationOutcome<User?>>();
        
        return result;
    }
}