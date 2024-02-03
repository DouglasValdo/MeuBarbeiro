using Domain.Common.Service;
using MobileUI.Objects.ViewModels;

namespace MobileUI.Controls.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage(IApplicationService appService)
    {
        InitializeComponent();
        BindingContext = new LoginPageViewModel(appService);
    }
}