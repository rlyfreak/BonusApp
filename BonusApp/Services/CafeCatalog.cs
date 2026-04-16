using BonusApp.Models;
using Microsoft.Maui.Graphics;

namespace BonusApp.Services;

public static class CafeCatalog
{
    private static readonly IReadOnlyList<CafeCatalogEntry> _entries =
    [
        new CafeCatalogEntry
        {
            Id = 1,
            Name = "Калипсо",
            CardPrefix = "KO",
            QrCodeSource = "qr_code_kalipso.svg",
            FullCardArtSource = "kalipsologo.svg",
            BadgeWidth = 116
        },
        new CafeCatalogEntry
        {
            Id = 2,
            Name = "Розмарин",
            CardPrefix = "RN",
            QrCodeSource = "qr_code_rozmarin.svg",
            CenterLogoSource = "rozmarinlogo.svg",
            BadgeBackgroundColor = Color.FromArgb("#E66D7383"),
            BadgeWidth = 120
        },
        new CafeCatalogEntry
        {
            Id = 3,
            Name = "Винил",
            CardPrefix = "VL",
            QrCodeSource = "qr_code_vinyl.svg",
            FullCardArtSource = "vinyllogo.svg",
            BadgeWidth = 110
        },
        new CafeCatalogEntry
        {
            Id = 4,
            Name = "Редакция",
            CardPrefix = "RA",
            QrCodeSource = "qr_code_redac.svg",
            FullCardArtSource = "redaktsyalogo.svg",
            BadgeWidth = 110,
            FullCardArtScale = 1.14
        },
        new CafeCatalogEntry
        {
            Id = 5,
            Name = "Знак",
            CardPrefix = "ZK",
            QrCodeSource = "qr_code_znak.svg",
            FullCardArtSource = "znaklogo.svg",
            BadgeWidth = 110,
            FullCardArtScale = 1.10
        },
        new CafeCatalogEntry
        {
            Id = 6,
            Name = "Комод",
            CardPrefix = "KD",
            QrCodeSource = "qr_code_komod.svg",
            FullCardArtSource = "komodlogo.svg",
            BadgeWidth = 110
        }
    ];

    public static IReadOnlyList<CafeCatalogEntry> GetAll() => _entries;

    public static CafeCatalogEntry? GetById(int id) => _entries.FirstOrDefault(entry => entry.Id == id);

    public static CafeCatalogEntry? GetByName(string cafeName) =>
        _entries.FirstOrDefault(entry => string.Equals(entry.Name, cafeName, StringComparison.Ordinal));
}
