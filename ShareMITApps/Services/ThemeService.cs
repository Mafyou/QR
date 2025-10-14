namespace ShareMITApps.Services;

public interface IThemeService
{
    AppTheme CurrentTheme { get; }
    Task<AppTheme> GetThemeAsync();
    Task SetThemeAsync(AppTheme theme);
    event EventHandler<AppTheme> ThemeChanged;
}

public class ThemeService : IThemeService
{
    private const string ThemeKey = "app_theme";

    public AppTheme CurrentTheme { get; private set; }

    public event EventHandler<AppTheme>? ThemeChanged;

    public async Task<AppTheme> GetThemeAsync()
    {
        var theme = await SecureStorage.GetAsync(ThemeKey);

        CurrentTheme = theme switch
        {
            nameof(AppTheme.Dark) => AppTheme.Dark,
            nameof(AppTheme.Light) => AppTheme.Light,
            _ => AppTheme.Unspecified
        };

        return CurrentTheme;
    }

    public async Task SetThemeAsync(AppTheme theme)
    {
        CurrentTheme = theme;

        await SecureStorage.SetAsync(ThemeKey, theme.ToString());

        if (Application.Current is not null)
        {
            Application.Current.UserAppTheme = theme;
        }

        ThemeChanged?.Invoke(this, theme);
    }
}