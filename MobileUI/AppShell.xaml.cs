using MobileUI.Objects.Extensions;
using MobileUI.Objects.Session;

namespace MobileUI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        //check if user is already logged in
         //var loggedUser = await SessionManager.GetCurrentUser();
        
         //if (loggedUser != null ) await Current.GoToAsync("//HomeTab");
        
        base.OnAppearing();
    }

    protected override async void OnNavigating(ShellNavigatingEventArgs args)
    {
        await this.ExecuteLogOutIfNecessary();
        base.OnNavigating(args);
    }
}