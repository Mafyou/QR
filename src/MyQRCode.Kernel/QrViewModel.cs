namespace MyQRCode.Kernel;

public class QrViewModel(IQrCodeGeneratorService qrCodeGeneratorService, IFileSaveService fileSaveService)
{
    public byte[]? LastQrCodeBytes { get; private set; }
    public string SearchTerm { get; set; } = string.Empty;
    public bool IsScanVisible { get; private set; }
    public string ScanResult { get; private set; } = string.Empty;

    public byte[]? GenerateQrCode()
    {
        if (string.IsNullOrWhiteSpace(SearchTerm))
        {
            LastQrCodeBytes = null;
            return null;
        }

        LastQrCodeBytes = qrCodeGeneratorService.GenerateQrCode(SearchTerm);
        return LastQrCodeBytes;
    }

    public async Task<string?> SaveQrCodeAsync(CancellationToken stoppingToken = default)
    {
        if (LastQrCodeBytes is null)
            return null;

        var fileName = $"QRCode_{SearchTerm.Replace(' ', '_')}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
        return await fileSaveService.SaveAsync(LastQrCodeBytes, fileName, stoppingToken);
    }

    public void ToggleScan()
    {
        IsScanVisible = !IsScanVisible;
        if (!IsScanVisible)
            ScanResult = string.Empty;
    }

    public void OnBarcodeDetected(string result)
    {
        ScanResult = result;
        IsScanVisible = false;
    }
}
