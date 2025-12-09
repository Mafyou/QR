using QRCoder;
using ZXing.Net.Maui;

namespace MyQRCode;

public partial class MainPage : ContentPage
{
    private bool _isScanVisible = false;
    private byte[]? _lastQrCodeBytes;
    private string _searchTerm = string.Empty;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        // Existing counter logic
    }

    private void OnGenerateQrClicked(object sender, EventArgs e)
    {
        _searchTerm = inputEntry.Text;
        if (string.IsNullOrWhiteSpace(_searchTerm))
        {
            imageQRCode.Source = null;
            qrCodeBorder.IsVisible = false;
            downloadQrButton.IsVisible = false;
            _lastQrCodeBytes = null;
            return;
        }

        // Generate QR code as PNG
        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode(_searchTerm, QRCodeGenerator.ECCLevel.Q);
        using var qrCode = new PngByteQRCode(qrCodeData);
        var qrCodeBytes = qrCode.GetGraphic(20);
        imageQRCode.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
        qrCodeBorder.IsVisible = true;
        downloadQrButton.IsVisible = true;
        _lastQrCodeBytes = qrCodeBytes;
    }

    private async void OnDownloadQrClicked(object sender, EventArgs e)
    {
        if (_lastQrCodeBytes is null)
        {
            await DisplayAlertAsync("Erreur", "Aucun QR Code à télécharger.", "OK");
            return;
        }

        var fileName = $"QRCode_{_searchTerm.Replace(' ', '_')}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
#if ANDROID
        var picturesPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures)?.AbsolutePath;
        var qrCodesDir = Path.Combine(picturesPath ?? FileSystem.Current.AppDataDirectory, "QRCodes");
        if (!Directory.Exists(qrCodesDir))
            Directory.CreateDirectory(qrCodesDir);
        var filePath = Path.Combine(qrCodesDir, fileName);
#elif IOS || MACCATALYST
        var picturesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        var qrCodesDir = Path.Combine(picturesPath, "QRCodes");
        if (!Directory.Exists(qrCodesDir))
            Directory.CreateDirectory(qrCodesDir);
        var filePath = Path.Combine(qrCodesDir, fileName);
#else
        var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
#endif
        try
        {
            File.WriteAllBytes(filePath, _lastQrCodeBytes);
            await DisplayAlertAsync("Succès", $"QR Code enregistré dans les Photos : {filePath}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erreur", $"Impossible d'enregistrer le QR Code : {ex.Message}", "OK");
        }
    }

    private void OnToggleScanClicked(object sender, EventArgs e)
    {
        _isScanVisible = !_isScanVisible;
        scanBorder.IsVisible = _isScanVisible;
        qrScanner.IsDetecting = _isScanVisible;
        qrScanner.IsVisible = _isScanVisible;
        toggleScanButton.Text = _isScanVisible ? "Masquer la caméra" : "Afficher la caméra";
        if (!_isScanVisible)
            scanResultLabel.Text = "Le résultat du scan apparaîtra ici.";
        else
            scanResultLabel.Text = "Scannez un QR code...";
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
                scanBorder.IsVisible = false;
                _isScanVisible = false;
                toggleScanButton.Text = "Afficher la caméra";
            });
        }
    }
}