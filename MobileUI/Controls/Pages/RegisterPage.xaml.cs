using Domain.Common.Service;
using Microsoft.Extensions.Logging;
using MobileUI.Objects.ViewModels;

namespace MobileUI.Controls.Pages;

public partial class RegisterPage : BasePage
{
    public RegisterPage(IApplicationService appService, ILogger<RegisterPage> logger):base(appService)
    {
        InitializeComponent();
        BindingContext = new RegisterPageViewModel(appService, logger);
    }
}