using Domain.Common.Service;
using UraniumUI.Pages;

namespace MobileUI.Controls;

public abstract class BasePage(IApplicationService service) : UraniumContentPage 
{
    public  IApplicationService ApplicationService { get; } = service;
}