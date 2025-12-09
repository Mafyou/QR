# MyQRCode.Web

Ce projet est une application Blazor WebAssembly (.NET 10) permettant de générer des QR codes.

## Démarrage local

1. Installez le SDK .NET 10 : https://dotnet.microsoft.com/download/dotnet/10.0
2. Ouvrez un terminal à la racine du projet.
3. Lancez la commande suivante :
   ```sh
   dotnet run --project MyQRCode.Web/MyQRCode.Web.csproj
   ```
4. Accédez à l’application via http://localhost:5000 ou l’URL indiquée dans le terminal.

## Déploiement Azure Static Web Apps

Le déploiement est automatisé via GitHub Actions :
- Le workflow `.github/workflows/staticwebapp.yml` compile et publie le projet Blazor WebAssembly sur Azure Static Web Apps.
- Les paramètres principaux sont :
  - `app_location`: `./MyQRCode.Web` (chemin du projet Blazor)
  - `output_location`: `wwwroot` (dossier de sortie du build)

## Structure du projet

- `MyQRCode.Web/` : Projet Blazor WebAssembly (.NET 10)
- `.github/workflows/staticwebapp.yml` : Workflow CI/CD pour Azure Static Web Apps

## Génération de QR Code

La bibliothèque [QRCoder](https://github.com/codebude/QRCoder) est utilisée pour générer les QR codes.

## Prérequis
- .NET 10 SDK
- Compte Azure avec accès à Static Web Apps

## Variables secrètes
- Le token `AZURE_STATIC_WEB_APPS_API_TOKEN_ICY_STONE_0BB06C303` doit être défini dans les secrets du dépôt GitHub.