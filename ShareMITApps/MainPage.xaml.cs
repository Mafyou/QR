using ShareMITApps.ViewModels;

namespace ShareMITApps;

public partial class MainPage : ContentPage
{
    public MainPage(MainVewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
