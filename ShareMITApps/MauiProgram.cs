using ShareMITApps.Services;

namespace ShareMITApps;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        // Register services
        builder.Services.AddSingleton<IThemeService, ThemeService>();
        
        // Register pages and view models
        builder.Services.AddTransientWithShellRoute<MainPage, MainVewModel>(nameof(MainPage));

        return builder.Build();
    }
}
