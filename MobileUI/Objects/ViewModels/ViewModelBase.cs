using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Common.Service;
using Microsoft.Extensions.Logging;

// ReSharper disable All

namespace MobileUI.Objects.ViewModels;

public class ViewModelBase: ObservableObject
{
    protected IApplicationService AppServiceProvider { get; }
    protected ILogger Logger { get; }

    public ViewModelBase(IApplicationService appServiceProvider, ILogger logger)
    {
        AppServiceProvider = appServiceProvider;
        Logger = logger;
    }
}