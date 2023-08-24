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
        mauiAppBuilder.Services.AddSingleton<TrackingViewModel>();
        mauiAppBuilder.Services.AddTransient<TrackingView>();
        return mauiAppBuilder;
    }
}