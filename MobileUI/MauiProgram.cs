using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
#if ANDROID
using MobileUI.Platforms.Android.Handlers;
#endif
using MobileUI.Objects.Extensions;

using Mopups.Hosting;
using UraniumUI;

namespace MobileUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .LoadViewsAndViewModels()
            .LoadAppService()
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseUraniumUI()
            .UseUraniumUIMaterial()
            .UseUraniumUIBlurs()
            .LoadSerilog()
            .ConfigureMopups()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemi-bold");
                fonts.AddFontAwesomeIconFonts();
                fonts.AddMaterialIconFonts();
            });
        
        //This is necessary because .net8 is positioning the refresh indicator in the wrong location
        //with this code we modifying the refreshing position 
        builder.ConfigureMauiHandlers(collections =>
        {
#if ANDROID
            collections.AddHandler(typeof(RefreshView), typeof(RefreshViewCustomHandler));
#endif
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}