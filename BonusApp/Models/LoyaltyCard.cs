using Microsoft.Maui.Graphics;

namespace BonusApp.Models;

public class LoyaltyCard
{
    public int Id { get; set; }
    public string CafeName { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    public int BonusBalance { get; set; }
    public string QrCodeValue { get; set; } = string.Empty;

    // Карточки, которые уже являются готовым прямоугольным ассетом
    public bool UseFullCardArt => CafeName is "Калипсо" or "Комод" or "Винил" or "Редакция" or "Знак";

    // Карточки, где нужен белый фон + отдельный логотип
    public bool UseCenteredLogoCard => CafeName == "Розмарин";

    public bool UseFallbackCard => !UseFullCardArt && !UseCenteredLogoCard;

    public string FullCardArtSource => CafeName switch
    {
        "Калипсо" => "kalipsologo.svg",
        "Комод" => "komodlogo.svg",
        "Винил" => "vinyllogo.svg",
        "Редакция" => "redaktsyalogo.svg",
        "Знак" => "znaklogo.svg",
        _ => string.Empty
    };

    public string CenterLogoSource => CafeName switch
    {
        "Розмарин" => "rozmarinlogo.svg",
        _ => string.Empty
    };

    public Color BadgeBackgroundColor => CafeName switch
    {
        "Розмарин" => Color.FromArgb("#E66D7383"),
        _ => Color.FromArgb("#E64C5263")
    };

    public double BadgeWidth => CafeName switch
    {
        "Розмарин" => 120,
        "Калипсо" => 116,
        "Комод" => 110,
        "Знак" => 110,
        "Винил" => 110,
        "Редакция" => 110,
        _ => 116
    };

    public double FullCardArtScale => CafeName switch
    {
        "Редакция" => 1.14,
        "Знак" => 1.10,
        _ => 1.0
    };

    public string FallbackTextColor => "#2F3648";

    public string QrCodeSource { get; set; } = string.Empty;
}