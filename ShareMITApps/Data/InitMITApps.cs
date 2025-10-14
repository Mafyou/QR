namespace ShareMITApps.Data;

public record InitMITApps
{
    public IReadOnlyCollection<MITApp> MyMITApps { get; set; }
    public InitMITApps()
    {
        MyMITApps =
            [
                new MITApp {
                    Name = "QR Actus",
                    Category = Category.Culture,
                    AppLink = "https://play.google.com/store/apps/details?id=fr.mattd.qractus",
                    ImageUrl = "qractus.png"
                },
                new MITApp {
                    Name = "Fit Chronos",
                    Category = Category.DevPerso,
                    AppLink = "https://play.google.com/store/apps/details?id=fr.mattd.fit",
                    ImageUrl = "fit.png"
                },
                new MITApp {
                    Name = "Boost Your Mind",
                    Category = Category.DevPerso,
                    AppLink = "https://play.google.com/store/apps/details?id=fr.mattd.bymapp",
                    ImageUrl = "bym.png"
                },
                new MITApp {
                    Name = "Balades Piétonnes",
                    Category = Category.Culture,
                    AppLink = "https://play.google.com/store/apps/details?id=fr.mafyou.btonnes",
                    ImageUrl = "btonnes.png"
                },
                new MITApp {
                    Name = "Commandements",
                    Category = Category.DevPerso,
                    AppLink = "https://play.google.com/store/apps/details?id=fr.mafyou.commandements",
                    ImageUrl = "commandements.png"
                },
                new MITApp {
                    Name = "Fast Notes",
                    Category = Category.ChargeMentale,
                    AppLink = "https://play.google.com/store/apps/details?id=fr.mattd.notes",
                    ImageUrl = "fastnotes.png"
                },
                new MITApp {
                    Name = "Rechercher dans vos favoris",
                    Category = Category.Culture,
                    AppLink = "https://play.google.com/store/apps/details?id=fr.mafyou.multisearches",
                    ImageUrl = "tbbi.png"
                },
                new MITApp {
                    Name = "Aide au Poker",
                    Category = Category.ChargeMentale,
                    AppLink = "https://play.google.com/store/apps/details?id=fr.mafyou.aideaupoker",
                    ImageUrl = "poker.png"
                },
            ];
    }
}