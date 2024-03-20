using ApostlePath.View;
using ApostlePath.ViewModel;
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

            builder.Services.AddViewModel<QuestViewModel, QuestPage>();
#endif

            return builder.Build();
        }

        private static void AddViewModel<TViewModel, TView>(this IServiceCollection services) where TView : ContentPage, new() where TViewModel : class
        {
            services.AddTransient<TViewModel>();
            services.AddTransient(s => new TView() { BindingContext = s.GetRequiredService<TViewModel>()});
        }
    }
}
