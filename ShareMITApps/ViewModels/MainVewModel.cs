using ShareMITApps.Services;

namespace ShareMITApps.ViewModels;

public partial class MainVewModel : ObservableObject
{
    private readonly IThemeService _themeService;

    [ObservableProperty]
    private bool _isExpanded;
    [ObservableProperty]
    private ObservableCollection<MITApp> _apps = [];
    [ObservableProperty]
    private bool _isDarkTheme;
    [ObservableProperty]
    private string _themeIcon = "☀️";

    public ObservableCollection<MITAppGroup> GroupedApps { get; set; }

    public MainVewModel(IThemeService themeService)
    {
        _themeService = themeService;
        
        Apps = new(new InitMITApps().MyMITApps);
        GroupedApps = new ObservableCollection<MITAppGroup>(
            Apps
                .GroupBy(a => a.Category)
                .Select(g => new MITAppGroup(g.Key, g))
                .OrderBy(o => o.Category)
        );

        InitializeTheme();
    }

    [RelayCommand]
    private void OnToggleExpander()
        => IsExpanded = !IsExpanded;

    [RelayCommand]
    private async Task ToggleTheme()
    {
        var newTheme = IsDarkTheme ? AppTheme.Light : AppTheme.Dark;
        await _themeService.SetThemeAsync(newTheme);
        
        IsDarkTheme = newTheme == AppTheme.Dark;
        ThemeIcon = IsDarkTheme ? "🌙" : "☀️";
    }

    private async void InitializeTheme()
    {
        var currentTheme = await _themeService.GetThemeAsync();
        IsDarkTheme = currentTheme == AppTheme.Dark;
        ThemeIcon = IsDarkTheme ? "🌙" : "☀️";
    }
}