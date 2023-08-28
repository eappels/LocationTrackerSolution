﻿using LocationTracker.Services;
using LocationTracker.Services.Interfaces;
using LocationTracker.ViewModels;
using LocationTracker.Views;
using Microsoft.Extensions.Logging;

namespace LocationTracker;

public static class MauiProgram
{

    public static MauiApp CreateMauiApp() => MauiApp.CreateBuilder()
        .UseMauiApp<App>()
        .ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        })
        .UseMauiMaps()
        .Register()
        .Build();

    public static MauiAppBuilder Register(this MauiAppBuilder mauiAppBuilder)
    {
#if DEBUG
        mauiAppBuilder.Logging.AddDebug();
#endif
        mauiAppBuilder.Services.AddSingleton<IDBService, DBService>();
        mauiAppBuilder.Services.AddSingleton<LocationService>();
        mauiAppBuilder.Services.AddSingleton<TrackingViewModel>();
        mauiAppBuilder.Services.AddSingleton<HistoryViewModel>();
        mauiAppBuilder.Services.AddTransient<TrackingView>();
        mauiAppBuilder.Services.AddTransient<HistoryView>();
        return mauiAppBuilder;
    }
}