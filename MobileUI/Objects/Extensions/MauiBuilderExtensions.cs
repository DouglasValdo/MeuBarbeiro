using ApplicationStructure.Services;
using Domain.Common.Service;
using MetroLog.MicrosoftExtensions;
using MetroLog.Operators;
using Microsoft.Extensions.Logging;
using MobileUI.Controls.Pages;
using MobileUI.Controls.Tabs;
using MobileUI.Objects.ViewModels;
using MobileUI.Service;

namespace MobileUI.Objects.Extensions;

public static class MauiBuilderExtensions   
{
    public static MauiAppBuilder LoadAppService(this MauiAppBuilder builder)
    {
        builder.Services
            .AddTransient<IApplicationService>((s) 
                => new ApplicationServiceEndpoints(AppApiHttpClient.GetClient()));
        
        return builder;
    }
    
    public static MauiAppBuilder LoadViewsAndViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddScoped<LoginPageViewModel>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddScoped<RegisterPageViewModel>();
        builder.Services.AddTransient<RegisterPage>();
        builder.Services.AddScoped<HomeTab>();
        builder.Services.AddTransient<HomeTabViewModel>();
        
        return builder;
    }
    
    public static MauiAppBuilder LoadSerilog(this MauiAppBuilder builder)
    {
        builder.Logging.AddStreamingFileLogger(options =>
        {
            options.FolderPath  = $"{FileSystem.CacheDirectory}/Logs";
            options.MinLevel    = LogLevel.Trace;
        });
        
        builder.Services.AddSingleton(LogOperatorRetriever.Instance);

        return builder;
    }
    
}