namespace MyQRCode.Shared.UnitTests;

public class QrCodeGeneratorServiceTests
{
    private readonly IQrCodeGeneratorService _sut = new QrCodeGeneratorService();

    [Fact]
    public void GenerateQrCode_WithValidText_ReturnsPngBytes()
    {
        var result = _sut.GenerateQrCode("https://example.com");

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        // PNG magic bytes: 89 50 4E 47
        result[0].ShouldBe((byte)0x89);
        result[1].ShouldBe((byte)0x50);
        result[2].ShouldBe((byte)0x4E);
        result[3].ShouldBe((byte)0x47);
    }

    [Theory]
    [InlineData("Hello World")]
    [InlineData("https://github.com/Mafyou/QR")]
    [InlineData("texte avec accents éàü")]
    public void GenerateQrCode_WithVariousInputs_ReturnsNonEmptyBytes(string text)
    {
        var result = _sut.GenerateQrCode(text);

        result.ShouldNotBeNull();
        result.Length.ShouldBeGreaterThan(0);
    }
}
