namespace MyQRCode.Services;

public class FileSaveService : IFileSaveService
{
    public async Task<string> SaveAsync(byte[] data, string fileName, CancellationToken stoppingToken = default)
    {
#if ANDROID
        var picturesPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures)?.AbsolutePath;
        var qrCodesDir = Path.Combine(picturesPath ?? FileSystem.Current.AppDataDirectory, "QRCodes");
#elif IOS || MACCATALYST
        var picturesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        var qrCodesDir = Path.Combine(picturesPath, "QRCodes");
#else
        var qrCodesDir = FileSystem.Current.AppDataDirectory;
#endif
        if (!Directory.Exists(qrCodesDir))
            Directory.CreateDirectory(qrCodesDir);

        var filePath = Path.Combine(qrCodesDir, fileName);
        await File.WriteAllBytesAsync(filePath, data, stoppingToken);
        return filePath;
    }
}
