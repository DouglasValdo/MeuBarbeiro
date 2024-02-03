using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Common.Service;

// ReSharper disable All

namespace MobileUI.Objects.ViewModels;

public class ViewModelBase: ObservableObject
{
    protected IApplicationService AppServiceProvider { get; }

    public ViewModelBase(IApplicationService appServiceProvider) => AppServiceProvider = appServiceProvider;
}