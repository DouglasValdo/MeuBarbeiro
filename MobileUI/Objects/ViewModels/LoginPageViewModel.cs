using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Common.Service;
using Domain.Models.Service;
using Microsoft.Extensions.Logging;
using MobileUI.Objects.Helpers;
using MobileUI.Objects.Session;


namespace MobileUI.Objects.ViewModels;

public partial class LoginPageViewModel : ViewModelBase
{
    [ObservableProperty] private string _userPhoneNumber = string.Empty;
    public ICommand LoginCommand { get; }

    public LoginPageViewModel(IApplicationService appServiceProvider, ILogger logger) : base(appServiceProvider, logger)
    {
        LoginCommand = new AsyncRelayCommand(Login);
    }

    private async Task Login()
    {
        if (string.IsNullOrWhiteSpace(UserPhoneNumber)) return;

        var isValidPhoneNumber = ApplicationHelper.IsValidUserPhoneNumber(UserPhoneNumber);

        if (isValidPhoneNumber == false)
        {
            await ApplicationHelper.DisplayMessage("Invalid phone number.");
            return;
        }

        try
        {
            //get user from service by provided phoneNumber
            var serviceOutcome = await ApplicationHelper
                .ExecuteLoadingTask("Processing login...",
                    () => AppServiceProvider.UserService.GetUserByPhoneNumberAsync(UserPhoneNumber));

            //in case the result is null it means that we were not able to connect to the api
            //display some information for the user that it was not possible to perform current action
            if (serviceOutcome == null)
            {
                await ApplicationHelper.DisplayMessage("Some error has occured try again later.");
                return;
            }

            if (serviceOutcome.IsSuccessfully)
            {
                //unable to log the user. user not found
                // in that case we want to register the current user
                if (serviceOutcome.Result == null)
                    await ProcessGoToRegister(UserPhoneNumber);
                else
                    await ProcessGoToOtpPage(serviceOutcome.Result.Id);
            }
            else
            {
                Logger.Log(LogLevel.Information, 
                    "Unable to get user from service. Error: {message}", serviceOutcome.ErrorMessage);

                await ApplicationHelper.DisplayMessage("Some error has occured in login.");    
            }
        }
        catch (HttpRequestException serviceException)
        {
            //log to database what happen
            //display some information to the user
            Logger.Log(LogLevel.Error, serviceException, "Get UserFromPhoneNumber request to login throws error.");
            await ApplicationHelper.DisplayHttpRequestExceptionMessage(serviceException);
        }
        catch (Exception e)
        {
            //log to database what happen
            //display some information to the user
            Logger.Log(LogLevel.Error, e, "Login page throws unknown error.");
            await ApplicationHelper.DisplayMessage("Some error has occured.");
        }
    }

    private static async Task ProcessGoToOtpPage(Guid resultId)
    {
        //store user in the app current session
        await SessionManager.StoreUser(resultId);
        //navigate user to the home page and register infos in session for the current user
        var navToHomeParam = new ShellNavigationQueryParameters
        {
            { "GoToPage", "//HomeTab" }
        };
        await Shell.Current.GoToAsync("//OTPPage", navToHomeParam);
    }

    private static async Task ProcessGoToRegister(string userPhoneNumber)
    {
        var userToRegister = new UserModel
        {
            PhoneNumber = int.Parse(userPhoneNumber),
            IsDeleted =false
        };
                    
        var navToRegisterParam = new ShellNavigationQueryParameters
        {
            { "GoToPage", "//RegisterPage" },
            {"UserToRegister", userToRegister}
        };
                    
        await Shell.Current.GoToAsync("//OTPPage", navToRegisterParam);
    }
}