using Domain.Common.Service;
using Microsoft.Extensions.Logging;

namespace MobileUI.Objects.ViewModels;

public class RegisterPageViewModel : ViewModelBase
{
    public RegisterPageViewModel(IApplicationService appServiceProvider, ILogger logger) : base(appServiceProvider, logger)
    {
    }
}