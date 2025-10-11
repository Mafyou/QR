using CommunityToolkit.Mvvm.ComponentModel;
using QRCoder;
using System.Windows.Input;

namespace ShareMITApps.Models;

public partial class MITApp : ObservableObject
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
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
            using var qrCodeData = qrGenerator.CreateQrCode(Url, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);
            var qrCodeBytes = qrCode.GetGraphic(20);
            return ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
        }
    }
}