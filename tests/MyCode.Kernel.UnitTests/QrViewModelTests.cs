namespace MyCode.Kernel.UnitTests;

public class QrViewModelTests
{
    private readonly Mock<IQrCodeGeneratorService> _qrGeneratorMock = new();
    private readonly Mock<IFileSaveService> _fileSaveMock = new();
    private readonly QrViewModel _sut;

    public QrViewModelTests()
    {
        _sut = new QrViewModel(_qrGeneratorMock.Object, _fileSaveMock.Object);
    }

    [Fact]
    public void GenerateQrCode_WhenSearchTermIsEmpty_ReturnsNull()
    {
        _sut.SearchTerm = string.Empty;

        var result = _sut.GenerateQrCode();

        result.ShouldBeNull();
        _sut.LastQrCodeBytes.ShouldBeNull();
        _qrGeneratorMock.Verify(x => x.GenerateQrCode(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void GenerateQrCode_WhenSearchTermIsWhitespace_ReturnsNull()
    {
        _sut.SearchTerm = "   ";

        var result = _sut.GenerateQrCode();

        result.ShouldBeNull();
        _qrGeneratorMock.Verify(x => x.GenerateQrCode(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void GenerateQrCode_WithValidText_ReturnsBytes()
    {
        var expected = new byte[] { 1, 2, 3 };
        _sut.SearchTerm = "test";
        _qrGeneratorMock.Setup(x => x.GenerateQrCode("test")).Returns(expected);

        var result = _sut.GenerateQrCode();

        result.ShouldBe(expected);
        _sut.LastQrCodeBytes.ShouldBe(expected);
    }

    [Fact]
    public async Task SaveQrCodeAsync_WhenLastQrCodeBytesIsNull_ReturnsNull()
    {
        var result = await _sut.SaveQrCodeAsync();

        result.ShouldBeNull();
        _fileSaveMock.Verify(x => x.SaveAsync(It.IsAny<byte[]>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task SaveQrCodeAsync_WhenBytesAvailable_CallsFileSaveAndReturnsPath()
    {
        var bytes = new byte[] { 1, 2, 3 };
        _sut.SearchTerm = "hello";
        _qrGeneratorMock.Setup(x => x.GenerateQrCode("hello")).Returns(bytes);
        _fileSaveMock.Setup(x => x.SaveAsync(bytes, It.IsAny<string>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync("/path/to/QRCode_hello.png");
        _sut.GenerateQrCode();

        var result = await _sut.SaveQrCodeAsync();

        result.ShouldBe("/path/to/QRCode_hello.png");
        _fileSaveMock.Verify(x => x.SaveAsync(bytes, It.Is<string>(s => s.StartsWith("QRCode_hello")), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public void ToggleScan_WhenCalledOnce_SetIsScanVisibleToTrue()
    {
        _sut.ToggleScan();

        _sut.IsScanVisible.ShouldBeTrue();
    }

    [Fact]
    public void ToggleScan_WhenCalledTwice_SetIsScanVisibleToFalse()
    {
        _sut.ToggleScan();
        _sut.ToggleScan();

        _sut.IsScanVisible.ShouldBeFalse();
        _sut.ScanResult.ShouldBeEmpty();
    }

    [Fact]
    public void OnBarcodeDetected_SetsScanResultAndHidesScanner()
    {
        _sut.ToggleScan();

        _sut.OnBarcodeDetected("https://github.com");

        _sut.ScanResult.ShouldBe("https://github.com");
        _sut.IsScanVisible.ShouldBeFalse();
    }
}
