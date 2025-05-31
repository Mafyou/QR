window.qrScanner = {
    start: function (elementId, dotNetHelper) {
        const html5QrCode = new Html5Qrcode(elementId);
        html5QrCode.start(
            { facingMode: "environment" },
            {
                fps: 10,
                qrbox: 250,
            },
            qrCodeMessage => {
                dotNetHelper.invokeMethodAsync('OnQrCodeScanned', qrCodeMessage);
                html5QrCode.stop();
            }
        );
    }
};