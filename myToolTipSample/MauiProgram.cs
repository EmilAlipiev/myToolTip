 

using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Platform;
using Plugin.myToolTip;

namespace myTooltipSample;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureEffects(effects =>
            {
                effects.Add<ToolTipEffect, myToolTipImplementation>();
            })
            .ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}