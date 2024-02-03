using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Common.Service;
using MobileUI.Controls.Pages;
using MobileUI.Controls.Tabs;
using MobileUI.Objects.Helpers;
using MobileUI.Objects.Session;

namespace MobileUI.Objects.ViewModels;

public partial class LoginPageViewModel : ViewModelBase
{
    
    [ObservableProperty] private string _userPhoneNumber = string.Empty;
    public ICommand LoginCommand { get; }
    public ICommand GoToRegisterCommand { get; }
    public LoginPageViewModel(IApplicationService appServiceProvider):base(appServiceProvider)
    {
        LoginCommand = new RelayCommand(Login);
        GoToRegisterCommand = new RelayCommand(GoToRegister);
    }

    private async void Login()
    {
        if (string.IsNullOrWhiteSpace(UserPhoneNumber)) return;
        
        try
        {
            //get user from service by provided phoneNumber
            var serviceOutcome = await ApplicationHelper
                .ExecuteLoadingTask("processing login...",
                    () => AppServiceProvider.UserService.GetUserByPhoneNumberAsync(UserPhoneNumber));

            //in case the result is null it means that we were not able to connect to the api
            //display some information for the user that it was not possible to perform current action
            if (serviceOutcome == null)
            {
                await ApplicationHelper.DisplayError("Check your internet connection.");
                return;
            }
            
            //unable to log the user. probably wrong credentials
            //display that information to the user
            if (serviceOutcome.IsSucesseful == false || serviceOutcome.Result == null)
            {
                await ApplicationHelper.DisplayError("Unable to find account. Check your credentials.");
                return;
            }
            
            //store user in the app current session
            new SessionManager().StoreUser(serviceOutcome.Result.Id);
            //navigate user to the home page and register infos in session for the current user
            await Shell.Current.Navigation.PushAsync(new HomeTab());

        }
        catch (Exception e)
        {
            //log to database what happen
            //display some information to the user
            await ApplicationHelper.DisplayError("Some error has occured.");
        }
    }

    private async void GoToRegister()
    {
        await Shell.Current.Navigation.PushAsync(new RegisterPage());
    }

}
