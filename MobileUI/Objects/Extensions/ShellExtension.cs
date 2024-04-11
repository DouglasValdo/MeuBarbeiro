using MobileUI.Controls.Pages;
using MobileUI.Objects.Session;

namespace MobileUI.Objects.Extensions;

public static class ShellExtension
{
    public static async Task ExecuteLogOutIfNecessary(this Shell shell)
    {
        //check if user is already logged in
        var loggedUser = await SessionManager.GetCurrentUser();

        if (loggedUser != null) return;
        
        if(shell.CurrentPage.GetType() != typeof(LoginPage))
            await shell.GoToAsync($"//{nameof(LoginPage)}");
    }
}