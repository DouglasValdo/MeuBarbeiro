using System.Text;
using ApplicationStructure.Extensions;
using Domain.Common.Service;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Domain.Models.Service;
using Newtonsoft.Json;

namespace ApplicationStructure.Services.Endpoints;

public class UserEndpoint(HttpClient client)
    : EndpointsBase(client), IUserService
{
    private const string Endpoint = "User/";

    public async Task<OperationOutcome<User?>?> GetUserByPhoneNumberAsync(string phoneNumber)
    {
        var responseMessage = await Client.GetAsync($"{Endpoint}GetUserByPhoneNumber/{phoneNumber}");

        responseMessage.EnsureSuccessStatusCode();
        
        var result    = await responseMessage.Content.GetFromJsonAsync<OperationOutcome<User?>>();
        
        return result;
    }

    public async Task<OperationOutcome<User?>?> GetUserByIdAsync(Guid userId)
    {
        var responseMessage = await Client.GetAsync($"{Endpoint}GetUserById/{userId}");
        
        responseMessage.EnsureSuccessStatusCode();
        
        var result    = await responseMessage.Content.GetFromJsonAsync<OperationOutcome<User?>>();
        
        return result;
    }

    public async Task<OperationOutcome?> Register(UserModel user)
    {
        var serializedUser = JsonConvert.SerializeObject(user);

        var message = new StringContent(serializedUser, Encoding.UTF8, "application/json");
        
        var responseMessage = await Client.PostAsync($"{Endpoint}AddUser", message);
        
        responseMessage.EnsureSuccessStatusCode();
        
        var result    = await responseMessage.Content.GetFromJsonAsync<OperationOutcome>();
        
        return result;
    }
}