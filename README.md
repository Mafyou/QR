# MyQRCode

Ce depot contient les projets suivants :
- **MyQRCode.Web** : Application Blazor WebAssembly (.NET 10) pour la generation de QR codes, deployee sur Azure Static Web Apps.
- **MyQRCode** : Application .NET MAUI multi-plateforme (Android, iOS, MacCatalyst, Windows) pour la generation et la lecture de QR codes.
- **MyQRCode.Shared** : Bibliotheque partagee contenant le service de generation de QR codes (utilisee par MyQRCode.Web).
- **MyQRCode.Kernel** : Bibliotheque partagee contenant le ViewModel et les services communs (utilisee par MyQRCode MAUI).

---

## 1. MyQRCode.Web (Blazor WebAssembly)

Application web permettant de générer et scanner des QR codes avec logo intégré.

### Démarrage local

1. Installez le SDK .NET 10 : https://dotnet.microsoft.com/download/dotnet/10.0
2. Ouvrez un terminal a la racine du projet.
3. Lancez la commande suivante :
   ```sh
   dotnet run --project src/MyQRCode.Web/MyQRCode.Web.csproj
   ```
4. Accedez a l'application via http://localhost:5000 ou l'URL indiquee dans le terminal.

### Deploiement Azure Static Web Apps

Le deploiement est automatise via GitHub Actions :
- Le workflow `.github/workflows/staticgenerator.yml` compile et publie le projet Blazor WebAssembly sur Azure Static Web Apps.
- Les parametres principaux sont :
  - `app_location`: `./src/MyQRCode.Web` (chemin du projet Blazor)
  - `output_location`: `wwwroot` (dossier de sortie du build)

---

## 2. MyQRCode (MAUI)

Application mobile et desktop multi-plateforme (.NET MAUI) pour generer et scanner des QR codes avec logo integre.

### Plateformes supportees
- Android
- iOS
- MacCatalyst
- Windows

### Demarrage local

1. Installez le SDK .NET 10 et les outils MAUI : https://learn.microsoft.com/dotnet/maui/installation
2. Ouvrez un terminal a la racine du projet.
3. Lancez la commande suivante pour la plateforme souhaitee :
   ```sh
   # Android
   dotnet build src/MyQRCode/MyQRCode.csproj -t:Run -f net10.0-android

   # iOS (Mac requis)
   dotnet build src/MyQRCode/MyQRCode.csproj -t:Run -f net10.0-ios

   # MacCatalyst (Mac requis)
   dotnet build src/MyQRCode/MyQRCode.csproj -t:Run -f net10.0-maccatalyst

   # Windows
   dotnet build src/MyQRCode/MyQRCode.csproj -t:Run -f net10.0-windows10.0.19041.0
   ```

### Fonctionnalites
- Generation de QR codes avec logo centre (ZXing.Net.Maui)
- Scan de QR codes (ZXing.Net.Maui)

### Dependances principales
- `ZXing.Net.Maui` (generation et scan de QR codes)

---

## Structure du dépôt

```
src/
??? MyQRCode.Web/       # Projet Blazor WebAssembly (.NET 10)
??? MyQRCode/           # Projet .NET MAUI multi-plateforme
??? MyQRCode.Shared/    # Bibliothèque partagée (service QR code)
??? MyQRCode.Kernel/    # Bibliothèque partagée (ViewModel, services)
tests/
+-- MyQRCode.Shared.UnitTests/   # Tests unitaires du service QR code
+-- MyCode.Kernel.UnitTests/     # Tests unitaires du ViewModel
.github/workflows/
+-- staticgenerator.yml          # Workflow CI/CD pour Azure Static Web Apps
```

## Prerequis
- .NET 10 SDK : https://dotnet.microsoft.com/download/dotnet/10.0
- Outils MAUI (pour le projet mobile/desktop)
- Compte Azure avec acces a Static Web Apps (pour le deploiement web)

## Tests

Les tests unitaires utilisent **xUnit**, **Shouldly** et **Moq** conformement aux instructions Copilot.

### Executer les tests localement

```sh
# Tous les tests
dotnet test

# Avec couverture de code
dotnet test --collect:"XPlat Code Coverage"

# Tests d'un projet specifique
dotnet test tests/MyQRCode.Shared.UnitTests/MyQRCode.Shared.UnitTests.csproj
dotnet test tests/MyCode.Kernel.UnitTests/MyCode.Kernel.UnitTests.csproj
```

### CI/CD

Le workflow GitHub Actions (`.github/workflows/staticgenerator.yml`) execute automatiquement tous les tests avant le deploiement :
1. Restauration des dependances
2. Execution des tests avec couverture de code
3. Upload des rapports de couverture sur Codecov (optionnel)
4. Deploiement uniquement si les tests reussissent

## Variables secretes
- Le token `AZURE_STATIC_WEB_APPS_API_TOKEN_BLACK_GROUND_03A937E03` doit etre defini dans les secrets du depot GitHub.
- (Optionnel) Le token `CODECOV_TOKEN` peut etre ajoute pour publier les rapports de couverture de code sur Codecov.
