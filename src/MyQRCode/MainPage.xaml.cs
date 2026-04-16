namespace MyQRCode;

public partial class MainPage : ContentPage
{
    private readonly QrViewModel _viewModel;

    public MainPage(QrViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
    }

    private void OnGenerateQrClicked(object? sender, EventArgs e)
    {
        _viewModel.SearchTerm = inputEntry.Text;
        var bytes = _viewModel.GenerateQrCode();

        if (bytes is null)
        {
            imageQRCode.Source = null;
            qrCodeBorder.IsVisible = false;
            downloadQrButton.IsVisible = false;
            return;
        }

        imageQRCode.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
        qrCodeBorder.IsVisible = true;
        downloadQrButton.IsVisible = true;
    }

    private async void OnDownloadQrClicked(object? sender, EventArgs e)
    {
        try
        {
            var filePath = await _viewModel.SaveQrCodeAsync();
            if (filePath is null)
            {
                await DisplayAlertAsync("Erreur", "Aucun QR Code à télécharger.", "OK");
                return;
            }
            await DisplayAlertAsync("Succès", $"QR Code enregistré : {filePath}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erreur", $"Impossible d'enregistrer le QR Code : {ex.Message}", "OK");
        }
    }

    private void OnToggleScanClicked(object? sender, EventArgs e)
    {
        _viewModel.ToggleScan();
        scanBorder.IsVisible = _viewModel.IsScanVisible;
        qrScanner.IsDetecting = _viewModel.IsScanVisible;
        qrScanner.IsVisible = _viewModel.IsScanVisible;
        toggleScanButton.Text = _viewModel.IsScanVisible ? "Masquer la caméra" : "Afficher la caméra";
        scanResultLabel.Text = _viewModel.IsScanVisible ? "Scannez un QR code..." : "Le résultat du scan apparaîtra ici.";
    }

    private void OnBarcodeDetected(object? sender, BarcodeDetectionEventArgs e)
    {
        if (e.Results.Length > 0)
        {
            _viewModel.OnBarcodeDetected(e.Results[0].Value);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                scanResultLabel.Text = $"Scan result: {_viewModel.ScanResult}";
                qrScanner.IsDetecting = false;
                qrScanner.IsVisible = false;
                scanBorder.IsVisible = false;
                toggleScanButton.Text = "Afficher la caméra";
            });
        }
    }
}
