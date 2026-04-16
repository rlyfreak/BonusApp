using BonusApp.Services;
using Microsoft.Maui.Graphics;

namespace BonusApp.Models;

public class LoyaltyCard
{
    public int Id { get; set; }
    public string CafeName { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    public int BonusBalance { get; set; }
    public string QrCodeValue { get; set; } = string.Empty;
    public string QrCodeSource { get; set; } = string.Empty;

    private CafeCatalogEntry? CafeInfo => CafeCatalog.GetByName(CafeName);

    public bool UseFullCardArt => CafeInfo?.UseFullCardArt ?? false;

    public bool UseCenteredLogoCard => CafeInfo?.UseCenteredLogoCard ?? false;

    public bool UseFallbackCard => CafeInfo?.UseFallbackCard ?? true;

    public string FullCardArtSource => CafeInfo?.FullCardArtSource ?? string.Empty;

    public string CenterLogoSource => CafeInfo?.CenterLogoSource ?? string.Empty;

    public Color BadgeBackgroundColor => CafeInfo?.BadgeBackgroundColor ?? Color.FromArgb("#E64C5263");

    public double BadgeWidth => CafeInfo?.BadgeWidth ?? 116;

    public double FullCardArtScale => CafeInfo?.FullCardArtScale ?? 1.0;

    public string FallbackTextColor => CafeInfo?.FallbackTextColor ?? "#2F3648";
}
