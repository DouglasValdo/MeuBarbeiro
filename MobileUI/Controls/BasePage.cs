using Domain.Common.Service;

namespace MobileUI.Controls;

public abstract class BasePage(IApplicationService service) : ContentPage
{
    public  IApplicationService ApplicationService { get; } = service;
}