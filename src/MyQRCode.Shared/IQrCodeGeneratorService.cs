namespace MyQRCode.Shared;

public interface IQrCodeGeneratorService
{
    byte[] GenerateQrCode(string text);
}
