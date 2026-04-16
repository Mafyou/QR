window.qrCodeGenerator = {
    generate: function (elementId, text) {
        const container = document.getElementById(elementId);
        container.innerHTML = "";

        const tempDiv = document.createElement("div");
        tempDiv.style.display = "none";
        document.body.appendChild(tempDiv);

        new QRCode(tempDiv, {
            text: text,
            width: 200,
            height: 200,
            correctLevel: QRCode.CorrectLevel.H
        });

        setTimeout(function () {
            let qrCanvas = tempDiv.querySelector("canvas");
            if (!qrCanvas) {
                const img = tempDiv.querySelector("img");
                if (img) {
                    qrCanvas = document.createElement("canvas");
                    qrCanvas.width = 200;
                    qrCanvas.height = 200;
                    const ctx = qrCanvas.getContext("2d");
                    ctx.drawImage(img, 0, 0, 200, 200);
                }
            }

            if (qrCanvas) {
                const border = 5;
                const size = 200;
                const newSize = size + border * 2;
                const borderedCanvas = document.createElement("canvas");
                borderedCanvas.width = newSize;
                borderedCanvas.height = newSize;
                const ctx = borderedCanvas.getContext("2d");

                ctx.fillStyle = "#fff";
                ctx.fillRect(0, 0, newSize, newSize);
                ctx.drawImage(qrCanvas, border, border, size, size);

                const logo = new Image();
                logo.onload = function () {
                    const logoSize = newSize * 0.22;
                    const logoX = (newSize - logoSize) / 2;
                    const logoY = (newSize - logoSize) / 2;

                    // White background square behind logo
                    const padding = 3;
                    ctx.fillStyle = "#fff";
                    ctx.fillRect(logoX - padding, logoY - padding, logoSize + padding * 2, logoSize + padding * 2);
                    ctx.drawImage(logo, logoX, logoY, logoSize, logoSize);

                    const dataUrl = borderedCanvas.toDataURL("image/png");
                    container.innerHTML = "";
                    const img = document.createElement("img");
                    img.src = dataUrl;
                    container.appendChild(img);
                };
                logo.onerror = function () {
                    const dataUrl = borderedCanvas.toDataURL("image/png");
                    container.innerHTML = "";
                    const img = document.createElement("img");
                    img.src = dataUrl;
                    container.appendChild(img);
                };
                logo.src = "images/logo.jpg";
            }

            document.body.removeChild(tempDiv);
        }, 0);
    }
};