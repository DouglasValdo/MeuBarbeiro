using CommunityToolkit.Maui;
using MauiIcons.Fluent;
using MauiIcons.Material;
using Microsoft.Extensions.Logging;
using MobileUI.Objects.Extensions;
using The49.Maui.BottomSheet;

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
            .UseFluentMauiIcons()
            .UseMaterialMauiIcons()
            .UseBottomSheet()
            .LoadSerilog()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}