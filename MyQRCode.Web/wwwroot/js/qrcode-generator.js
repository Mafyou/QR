window.qrCodeGenerator = {
    generate: function (elementId, text) {
        const container = document.getElementById(elementId);
        container.innerHTML = ""; // Clear previous QR

        // Create a temporary container for QRCode generation
        const tempDiv = document.createElement("div");
        tempDiv.style.display = "none";
        document.body.appendChild(tempDiv);

        // Generate QR code as canvas
        new QRCode(tempDiv, {
            text: text,
            width: 200,
            height: 200,
            correctLevel: QRCode.CorrectLevel.H
        });

        setTimeout(function () {
            let qrCanvas = tempDiv.querySelector("canvas");
            if (!qrCanvas) {
                // fallback: try to get image and draw it to canvas
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
                // Create a new canvas with border
                const border = 5;
                const size = 200;
                const newSize = size + border * 2;
                const borderedCanvas = document.createElement("canvas");
                borderedCanvas.width = newSize;
                borderedCanvas.height = newSize;
                const ctx = borderedCanvas.getContext("2d");

                // Fill with white
                ctx.fillStyle = "#fff";
                ctx.fillRect(0, 0, newSize, newSize);

                // Draw QR code in the center
                ctx.drawImage(qrCanvas, border, border, size, size);

                // Convert to base64 and display
                const dataUrl = borderedCanvas.toDataURL("image/png");
                container.innerHTML = "";
                const img = document.createElement("img");
                img.src = dataUrl;
                container.appendChild(img);
            }

            // Clean up
            document.body.removeChild(tempDiv);
        }, 0);
    }
};