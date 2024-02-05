using System.Globalization;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Common.Service;
using Microsoft.Extensions.Logging;
using MobileUI.Objects.Helpers;
using MobileUI.Objects.Session;
// ReSharper disable All

namespace MobileUI.Objects.ViewModels;

public partial class LoginPageViewModel : ViewModelBase
{
    
    [ObservableProperty] private string _userPhoneNumber = string.Empty;
    public ICommand LoginCommand { get; }
    public LoginPageViewModel(IApplicationService appServiceProvider,ILogger logger ):base(appServiceProvider, logger)
    {
        LoginCommand = new RelayCommand(Login);
    }

    private async void Login()
    {
        if (string.IsNullOrWhiteSpace(UserPhoneNumber)) return;

        var isValidPhoneNumber = IsValidUserPhoneNumber(UserPhoneNumber);

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

            //unable to log the user. probably wrong credentials or user not found
            if (serviceOutcome.IsSucesseful && serviceOutcome.Result == null)
            {
                var navToRegisterParam = new ShellNavigationQueryParameters { { "GoToPage", "//RegisterPage" } };
                await Shell.Current.GoToAsync("//OTPPage", navToRegisterParam);    
                return;
            }

            //store user in the app current session
            await new SessionManager().StoreUser(serviceOutcome.Result.Id);
            //navigate user to the home page and register infos in session for the current user
            var navToHomeParam = new ShellNavigationQueryParameters { { "GoToPage", "//HomeTab" } };
            await Shell.Current.GoToAsync("//OTPPage", navToHomeParam);

        }
        catch (HttpRequestException serviceException)
        {
            //log to database what happen
            //display some information to the user
            Logger.Log(LogLevel.Error, serviceException, "Service request to log in throws error.");
            
            await ApplicationHelper.DisplayMessage("Check your internet connection.");
        }
        catch (Exception e)
        {
            //log to database what happen
            //display some information to the user
            Logger.Log(LogLevel.Error, e, "Login page throws unknown error.");
            await ApplicationHelper.DisplayMessage("Some error has occured.");
        }
    }

    private static bool IsValidUserPhoneNumber(string userPhoneNumber)
    {
        var isValidInt = int.TryParse(userPhoneNumber, 
            NumberStyles.Number, CultureInfo.InvariantCulture, out _);

        return isValidInt;
    }
}
