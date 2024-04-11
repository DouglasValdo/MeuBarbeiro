using ApplicationStructure.Services;
using Domain.Common.Service;
using MetroLog.MicrosoftExtensions;
using MetroLog.Operators;
using Microsoft.Extensions.Logging;
using MobileUI.Controls.Pages;
using MobileUI.Controls.Tabs;
using MobileUI.Objects.ViewModels;
using MobileUI.Service;
using UraniumUI;

namespace MobileUI.Objects.Extensions;

public static class MauiBuilderExtensions   
{
    public static MauiAppBuilder UseAppService(this MauiAppBuilder builder)
    {
        builder.Services
            .AddTransient<IApplicationService>((sp) 
                => new ApplicationServiceEndpoints(AppApiHttpClient.GetClient()));
        
        builder.Services.AddMopupsDialogs();
        
        return builder;
    }
    
    public static MauiAppBuilder UseViewsAndViewModels(this MauiAppBuilder builder)
    {
        //Pages
        builder.Services.AddScoped<LoginPageViewModel>();
        builder.Services.AddTransient<LoginPage>();
        
        builder.Services.AddScoped<RegisterPageViewModel>();
        builder.Services.AddTransient<RegisterPage>();
        
        builder.Services.AddTransient<ScheduleEditorPageViewModel>();
        builder.Services.AddScoped<ScheduleEditorPage>();
        
        //tabs
        builder.Services.AddScoped<HomeTab>();
        builder.Services.AddTransient<HomeTabViewModel>();
        
        builder.Services.AddScoped<BarberTabViewModel>();
        builder.Services.AddTransient<BarberTab>();
        
        builder.Services.AddScoped<SettingsTab>();
        builder.Services.AddTransient<SettingsTabViewModel>();
        
        return builder;
    }
    
    public static MauiAppBuilder UseLogger(this MauiAppBuilder builder)
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