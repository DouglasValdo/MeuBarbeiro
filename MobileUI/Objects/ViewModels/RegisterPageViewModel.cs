using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Common.Service;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Models.Service;
using Microsoft.Extensions.Logging;
using MobileUI.Objects.Helpers;

namespace MobileUI.Objects.ViewModels;

public partial class RegisterPageViewModel : ViewModelBase, IQueryAttributable
{
    private UserModel? _userToRegister;
    public ICommand RegisterCommand { get; }
    [ObservableProperty] private string _userProvidedFullName = string.Empty;
    public RegisterPageViewModel(IApplicationService appServiceProvider, ILogger logger) : base(appServiceProvider, logger)
    {
        RegisterCommand = new AsyncRelayCommand(Register);
    }

    private async Task Register()
    {
        if (string.IsNullOrWhiteSpace(UserProvidedFullName)) return;
        
        if(_userToRegister == null) throw new InvalidUserProvidedForRegister();

        try
        {
            _userToRegister.FullName = UserProvidedFullName;
            
            //call the user service to register a new user
            var registerUserServiceOutcome = await ApplicationHelper.ExecuteLoadingTask("Creating account", 
                () => AppServiceProvider.UserService.Register(_userToRegister));

            if (registerUserServiceOutcome == null)
            {
                await ApplicationHelper.DisplayMessage("Some error has occured try again later.");
                return;
            }

            if (registerUserServiceOutcome.IsSucesseful) await Shell.Current.GoToAsync("//HomeTab");
            else
            {
                Logger.Log(LogLevel.Information, 
                    "Unable to register user. Error: {message}", registerUserServiceOutcome.ErrorMessage);
                await ApplicationHelper.DisplayMessage("Some error has occured try again later.");
            }
            
        }catch (HttpRequestException serviceException)
        {
            //log to database what happen
            //display some information to the user
            Logger.Log(LogLevel.Error, serviceException, "Service not working try again later.");

            await ApplicationHelper.DisplayHttpRequestExceptionMessage(serviceException);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue("UserToRegister", out var value)) throw new InvalidUserProvidedForRegister();

        _userToRegister = (UserModel)value;
    }
}