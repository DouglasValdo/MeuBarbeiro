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
         var sessionManager = new SessionManager();
        
         var loggedUser = await sessionManager.GetCurrentUser();
        
         if (loggedUser != null ) await Current.GoToAsync("//HomeTab");
        
        base.OnAppearing();
    }

}