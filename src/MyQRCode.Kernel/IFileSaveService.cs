namespace MyQRCode.Kernel;

public interface IFileSaveService
{
    Task<string> SaveAsync(byte[] data, string fileName, CancellationToken stoppingToken = default);
}