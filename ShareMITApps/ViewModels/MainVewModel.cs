using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ShareMITApps.ViewModels;

public partial class MainVewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isExpanded;
    [ObservableProperty]
    private ObservableCollection<MITApp> _apps = [];
    public MainVewModel()
    {
        Apps = new(new InitMITApps().MyMITApps);
    }
    [RelayCommand]
    private void OnToggleExpander()
        => IsExpanded = !IsExpanded;
}