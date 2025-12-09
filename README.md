# MyQRCode

Ce dépôt contient deux projets principaux :
- **MyQRCode.Web** : Application Blazor WebAssembly (.NET 10) pour la génération de QR codes.
- **MyQRCode** : Application .NET MAUI multi-plateforme (Android, iOS, MacCatalyst, Windows) pour la génération et la lecture de QR codes.

---

## 1. MyQRCode.Web (Blazor WebAssembly)

Application web permettant de générer des QR codes.

### Démarrage local

1. Installez le SDK .NET 10 : https://dotnet.microsoft.com/download/dotnet/10.0
2. Ouvrez un terminal à la racine du projet.
3. Lancez la commande suivante :
   ```sh
   dotnet run --project MyQRCode.Web/MyQRCode.Web.csproj
   ```
4. Accédez à l’application via http://localhost:5000 ou l’URL indiquée dans le terminal.

### Déploiement Azure Static Web Apps

Le déploiement est automatisé via GitHub Actions :
- Le workflow `.github/workflows/staticwebapp.yml` compile et publie le projet Blazor WebAssembly sur Azure Static Web Apps.
- Les paramètres principaux sont :
  - `app_location`: `./MyQRCode.Web` (chemin du projet Blazor)
  - `output_location`: `wwwroot` (dossier de sortie du build)

---

## 2. MyQRCode (MAUI)

Application mobile et desktop multi-plateforme (.NET MAUI) pour générer et scanner des QR codes.

### Plateformes supportées
- Android
- iOS
- MacCatalyst
- Windows

### Démarrage local

1. Installez le SDK .NET 10 et les outils MAUI : https://learn.microsoft.com/dotnet/maui/installation
2. Ouvrez un terminal à la racine du projet.
3. Lancez la commande suivante pour la plateforme souhaitée :
   ```sh
   # Android
   dotnet build MyQRCode/MyQRCode.csproj -t:Run -f net10.0-android

   # iOS (Mac requis)
   dotnet build MyQRCode/MyQRCode.csproj -t:Run -f net10.0-ios

   # MacCatalyst (Mac requis)
   dotnet build MyQRCode/MyQRCode.csproj -t:Run -f net10.0-maccatalyst

   # Windows
   dotnet build MyQRCode/MyQRCode.csproj -t:Run -f net10.0-windows10.0.19041.0
   ```

### Fonctionnalités
- Génération de QR codes (QRCoder)
- Scan de QR codes (ZXing.Net.Maui)

### Dépendances principales
- `QRCoder` (génération de QR codes)
- `ZXing.Net.Maui` (scan de QR codes)

---

## Structure du dépôt

- `MyQRCode.Web/` : Projet Blazor WebAssembly (.NET 10)
- `MyQRCode/` : Projet .NET MAUI multi-plateforme
- `.github/workflows/staticwebapp.yml` : Workflow CI/CD pour Azure Static Web Apps

## Prérequis
- .NET 10 SDK
- Outils MAUI (pour le projet mobile/desktop)
- Compte Azure avec accès à Static Web Apps (pour le déploiement web)

## Variables secrètes
- Le token `AZURE_STATIC_WEB_APPS_API_TOKEN_ICY_STONE_0BB06C303` doit être défini dans les secrets du dépôt GitHub.