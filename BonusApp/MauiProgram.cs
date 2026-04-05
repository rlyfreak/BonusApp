using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace BonusApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                fonts.AddFont("Rubik-Regular.ttf", "RubikRegular");
                fonts.AddFont("Rubik-Medium.ttf", "RubikMedium");

            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}