using ApplicationStructure.Services;
using Domain.Common.Service;
using MobileUI.Controls.Pages;
using MobileUI.Objects.ViewModels;
using MobileUI.Service;

namespace MobileUI.Objects.Extensions;

public static class MauiBuilderExtensions
{
    public static MauiAppBuilder LoadAppService(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IApplicationService>
            (new ApplicationServiceEndpoints(AppApiHttpClient.GetClient()));
        
        return builder;
    }
    
    public static MauiAppBuilder LoadViewsAndViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddScoped<LoginPageViewModel>();
        builder.Services.AddTransient<LoginPage>();
        
        return builder;
    }
}