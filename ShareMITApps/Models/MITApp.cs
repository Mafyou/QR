namespace ShareMITApps.Models;

public partial class MITApp : ObservableObject
{
    public string Name { get; set; } = string.Empty;
    public string AppLink { get; set; } = string.Empty;
    public Category Category { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsExpanded { get; set; } = false;

    public ICommand ToggleExpanderCommand => new Command(() =>
    {
        IsExpanded = !IsExpanded;
        OnPropertyChanged(nameof(IsExpanded));
    });

    public ImageSource MyQRImageSource
    {
        get
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(AppLink, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);
            var qrCodeBytes = qrCode.GetGraphic(20);
            return ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
        }
    }
    [RelayCommand]
    public async Task OnOpenAppLink(string url)
        => await Launcher.OpenAsync(url);
}