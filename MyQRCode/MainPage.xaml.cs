using QRCoder;
using ZXing.Net.Maui;

namespace MyQRCode;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        // Existing counter logic
    }

    protected override void OnAppearing()
    {
        // How to translate a QRCOde sent on a stream to a string?
        // var qrCode = new QRCodeGenerator().CreateQrCode("Hello World", QRCodeGenerator.ECCLevel.Q);
        // var qrCodeBytes = new PngByteQRCode(qrCode).GetGraphic(20);
        // var qrCodeString = Convert.ToBase64String(qrCodeBytes);
        // var qrCodeImage = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
        // imageQRCode.Source = qrCodeImage;

    }

    private void OnGenerateQrClicked(object sender, EventArgs e)
    {
        var text = inputEntry.Text;
        if (string.IsNullOrWhiteSpace(text))
        {
            imageQRCode.Source = null;
            return;
        }

        // Generate QR code as PNG
        using (var qrGenerator = new QRCodeGenerator())
        using (var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q))
        using (var qrCode = new PngByteQRCode(qrCodeData))
        {
            var qrCodeBytes = qrCode.GetGraphic(20);
            imageQRCode.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
        }
    }

    private void OnScanQrClicked(object sender, EventArgs e)
    {
        qrScanner.IsVisible = true;
        qrScanner.IsDetecting = true;
        scanResultLabel.Text = "Scanning...";
    }

    private void OnBarcodeDetected(object sender, BarcodeDetectionEventArgs e)
    {
        if (e.Results.Length > 0)
        {
            var result = e.Results[0].Value;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                scanResultLabel.Text = $"Scan result: {result}";
                qrScanner.IsDetecting = false;
                qrScanner.IsVisible = false;
            });
        }
    }

    private void qrScanner_BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {

    }
}
