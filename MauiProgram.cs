using Microsoft.Extensions.Logging;

namespace ApostlePath
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Rubik.ttf", "Rubik");
                    fonts.AddFont("Rubik-Italic.ttf", "RubikItalic");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
