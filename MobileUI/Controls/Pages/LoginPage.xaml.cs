using Domain.Common.Service;
using Microsoft.Extensions.Logging;
using MobileUI.Objects.ViewModels;

namespace MobileUI.Controls.Pages;

public partial class LoginPage : BasePage
{
    public LoginPage(IApplicationService appService, ILogger<LoginPage> logger):base(appService)
    {
        InitializeComponent();
        BindingContext = new LoginPageViewModel(appService, logger);
    }
}