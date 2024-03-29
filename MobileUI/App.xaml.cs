﻿using MobileUI.Controls.Pages;
using MobileUI.Controls.Tabs;
using MobileUI.Handlers;

namespace MobileUI;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
        
        //Register application Routes
        MapRoutes();
        //Add BorderlessEntry Mappings
        BorderlessEntryHandler.AppendMapping();
    }

    private static void MapRoutes()
    {
        Routing.RegisterRoute(nameof(LoginPage),     typeof(LoginPage));
        Routing.RegisterRoute(nameof(RegisterPage),  typeof(RegisterPage));
        Routing.RegisterRoute(nameof(OTPPage),       typeof(OTPPage));
        Routing.RegisterRoute(nameof(HomeTab),       typeof(HomeTab));
        Routing.RegisterRoute(nameof(SettingsTab),   typeof(SettingsTab));
        Routing.RegisterRoute(nameof(ProfileTab),    typeof(ProfileTab));
        Routing.RegisterRoute(nameof(ScheduleEditorPage),    typeof(ScheduleEditorPage));
    }

}