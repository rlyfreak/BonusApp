using Microsoft.Maui.Graphics;

namespace BonusApp.Models;

public class CafeCatalogEntry
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string CardPrefix { get; init; } = string.Empty;
    public string QrCodeSource { get; init; } = string.Empty;
    public string? FullCardArtSource { get; init; }
    public string? CenterLogoSource { get; init; }
    public Color BadgeBackgroundColor { get; init; } = Color.FromArgb("#E64C5263");
    public double BadgeWidth { get; init; } = 116;
    public double FullCardArtScale { get; init; } = 1.0;
    public string FallbackTextColor { get; init; } = "#2F3648";

    public bool UseFullCardArt => !string.IsNullOrWhiteSpace(FullCardArtSource);
    public bool UseCenteredLogoCard => !string.IsNullOrWhiteSpace(CenterLogoSource);
    public bool UseFallbackCard => !UseFullCardArt && !UseCenteredLogoCard;
}
