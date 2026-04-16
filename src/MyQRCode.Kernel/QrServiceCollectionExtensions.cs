namespace MyQRCode.Kernel;

public static class QrServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddQrCoreServices()
        {
            services.AddSingleton<IQrCodeGeneratorService, QrCodeGeneratorService>();
            return services;
        }
    }
}